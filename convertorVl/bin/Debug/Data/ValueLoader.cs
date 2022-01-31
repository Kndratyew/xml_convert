using convertorVl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace convertorVl.Data
{
    public static class ValuteLoader
    {
        /// <summary>
        /// Получает список валют из текста XML
        /// </summary>
        /// <param name="XMLText">Строка с информацией о валютах в формате XML</param>
        /// <returns>Список валют</returns>
        public static List<Valute> LoadValutes(string XMLText) => XDocument.Parse(XMLText).Element("ValCurs").Elements("Valute").Select(x => new Valute()
        {
            CharCode = x.Element("CharCode").Value,
            Name = x.Element("Name").Value,
            Nominal = int.Parse(x.Element("Nominal").Value),
            Code = int.Parse(x.Element("NumCode").Value),
            Value = double.Parse(x.Element("Value").Value)
        }).ToList();
    }
}
