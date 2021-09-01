using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace cashConverter
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter amount of money you would like to deposit in DKK");
            string amountToExhange = Console.ReadLine();
            Console.WriteLine("Enter what type of currency code, you would like to exhange to");
            string codeChosen = Console.ReadLine();




            XDocument doc = XDocument.Load("https://www.nationalbanken.dk/_vti_bin/DN/DataService.svc/CurrencyRatesXML?lang=da");

            var currencies = doc.XPathSelectElements("//currency");

            List<Currency> listOfCurrencies = new List<Currency>();

            foreach (XElement element in currencies)
            {




                //                  ---Løsning uden en ekstra liste, klasse og loop---
                //decimal exhangedAmount = 0;

                //if (code == codeChosen)
                //{
                //    exhangedAmount = Decimal.Parse(amountToExhange) / (Decimal.Parse(element.Attribute("rate").Value) / 100);
                //    Console.WriteLine(exhangedAmount);

                //}



                //Henter værdierne fra XML filen og sætter den til variabler

                string code = element.Attribute("code").Value;

                string desc = element.Attribute("desc").Value;

                decimal rate = Decimal.Parse(element.Attribute("rate").Value);


                //Lavet et object af currency
                Currency currency = new Currency();

                //Bruger setter metoderne fra klassen til at give en value fra XML
                currency.Code = code;
                currency.Desc = desc;
                currency.Rate = rate;

                //Tilføjer alle vores currency fra XML filen til en liste som objekter
                listOfCurrencies.Add(currency);

            }

            foreach (Currency currency in listOfCurrencies)
            {
                decimal exhangedAmount = 0;

                if (codeChosen == currency.Code)
                {
                    exhangedAmount = Decimal.Parse(amountToExhange) / (currency.Rate / 100);
                    Console.WriteLine(exhangedAmount);
                    return;
                }



            }

        }
    }

    class Currency
    {
        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string desc;
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        private decimal rate;
        public decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }





    }

}






