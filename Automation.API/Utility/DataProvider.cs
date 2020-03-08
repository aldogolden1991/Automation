using AutoFixture;
using AutoFixture.AutoMoq;
using Automation.API.Model;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;

namespace Automation.API.Utility
{
    public static class DataProvider
    {
        public static T MoqData<T>()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            return fixture.Create<T>();
        }

        public static string GetConfiguration(string name)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var _config = new ConfigurationBuilder().AddJsonFile($"appsettings.{environment}.json").Build();
            return _config[name];
        }

        public static T ReadExcelFile<T>() where T : class
        {

            try
            {
                //Lets open the existing excel file and read through its content . Open the excel using openxml sdk
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(GetConfiguration("TestDataPath"), false))
                {
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    Sheets thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();
                    StringBuilder excelResult = new StringBuilder();
                    foreach (Sheet thesheet in thesheetcollection)
                    {
                        Worksheet theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;
                        SheetData thesheetdata = (SheetData)theWorksheet.GetFirstChild<SheetData>();
                        foreach (Row thecurrentrow in thesheetdata)
                        {
                            var flag = false;
                            foreach (Cell thecurrentcell in thecurrentrow)
                            {
                                
                                if (thecurrentcell.DataType != null)
                                {
                                    if (thecurrentcell.DataType == CellValues.SharedString)
                                    {

                                        int id;
                                        if (Int32.TryParse(thecurrentcell.InnerText, out id))
                                        {
                                            SharedStringItem item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                            if (item.Text != null)
                                            {
                                                if (typeof(T).Name.ToLower() == item.Text.Text.ToLower())
                                                {
                                                    flag = true;
                                                }
                                                else if (flag)
                                                {
                                                    return Serializer.DeserializeXml<T>(item.Text.Text);
                                                }
                                                else
                                                    break;
                                            }
                                            else if (item.InnerXml != null)
                                            {
                                                return Serializer.DeserializeXml<T>(item.InnerXml);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {

            }
            return null;
        }
    }
    public enum DataType
    {
        Json,
        Xml
    }
}
