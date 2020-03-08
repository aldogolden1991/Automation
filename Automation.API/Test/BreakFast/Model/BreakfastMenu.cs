using System.Collections.Generic;
using System.Xml.Serialization;

namespace Automation.API.Model
{

    [XmlRoot(ElementName = "food")]
    public class Food
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "price")]
        public string Price { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "calories")]
        public string Calories { get; set; }
    }

    [XmlRoot(ElementName = "breakfast_menu")]
    public class BreakfastMenu
    {
        [XmlElement(ElementName = "food")]
        public List<Food> Food { get; set; }
    }


}
