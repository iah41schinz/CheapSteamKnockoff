﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace SteamKnockoff
{
    public class Library
    {
        public List<Spiel> SpieleListe = new List<Spiel>();

        public string DefaultXmlPath = @"..\..\XmlSave.xml";

        public void SpielHinzufügen(string Titel, string Datum, string LetztesSpielDatum, string Installationspfad, string Kategorie, string Publisher, int USK)
         {
            //Es wird überprüft ob eines der Attribute null ist
            if (Titel == null || Datum == null || LetztesSpielDatum == null || Installationspfad == null || Kategorie == null || Publisher == null || USK != 0 && USK != 6 && USK != 12 && USK != 16 && USK != 18)
            {
                throw new NullReferenceException("Eines der übergebenen Parameter an Libary.SpielHinzufügen() hat eine Exception vom Typ NullReferenceException ausgelöst.");
            }
            //Es wird überprüft ob der Installationspfad Existiert#
            if (!File.Exists(Installationspfad))
            {
                throw new FileNotFoundException();
            }
            Spiel ISpiel = new Spiel();
            ISpiel.Titel = Titel;
            ISpiel.Datum = Datum;
            ISpiel.LetztesSpielDatum = LetztesSpielDatum;
            ISpiel.InstallationsPfad = Installationspfad;
            ISpiel.Kategorie = Kategorie;
            ISpiel.Publisher = Publisher;
            ISpiel.USK = USK;
            SpieleListe.Add(ISpiel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Gibt true zurück wenn das Speichern erfolgreich war und ansonsten false.</returns>
        public bool XmlSpeichern(string XmlPath)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode RootNode = doc.CreateElement("Spiele");
            doc.AppendChild(RootNode);
            for (int i = 0; i < SpieleListe.Count; i++)
            {
                RootNode.AppendChild(doc.CreateElement(SpieleListe[i].Titel.Replace(" ", "_")));
                RootNode.SelectSingleNode(SpieleListe[i].Titel.Replace(" ", "_")).Attributes.Append(doc.CreateAttribute("Datum")).InnerText = SpieleListe[i].Datum;
                RootNode.SelectSingleNode(SpieleListe[i].Titel.Replace(" ", "_")).Attributes.Append(doc.CreateAttribute("LetztesSpielDatum")).InnerText = SpieleListe[i].LetztesSpielDatum;
                RootNode.SelectSingleNode(SpieleListe[i].Titel.Replace(" ", "_")).Attributes.Append(doc.CreateAttribute("InstallationsPfad")).InnerText = SpieleListe[i].InstallationsPfad;
                RootNode.SelectSingleNode(SpieleListe[i].Titel.Replace(" ", "_")).Attributes.Append(doc.CreateAttribute("Kategorie")).InnerText = SpieleListe[i].Kategorie;
                RootNode.SelectSingleNode(SpieleListe[i].Titel.Replace(" ", "_")).Attributes.Append(doc.CreateAttribute("Publisher")).InnerText = SpieleListe[i].Publisher;
                RootNode.SelectSingleNode(SpieleListe[i].Titel.Replace(" ", "_")).Attributes.Append(doc.CreateAttribute("USK")).InnerText = SpieleListe[i].USK.ToString();
            }
            try
            {
                doc.Save(XmlPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool XmlLaden(string XmlPath)
        {
            int i = 0;
            if (!File.Exists(XmlPath))
            {
                File.Create(XmlPath);
                throw new FileNotFoundException("Savefile was not found. Creating new document");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlPath);
            //Root Knoten in ein XmlElement laden
            XmlElement RootNode = doc.DocumentElement;
            //Für jedes XML Element aus dem Root Knoten die Schleife ausführen
            try
            {
                foreach (XmlNode Spiel in RootNode.ChildNodes)
                {
                    SpieleListe[i].Titel = Spiel.Name;
                    SpieleListe[i].Datum = Spiel.Attributes["Datum"].InnerText;
                    SpieleListe[i].LetztesSpielDatum = Spiel.Attributes["LetztesSpielDatum"].InnerText;
                    SpieleListe[i].InstallationsPfad = Spiel.Attributes["InstallationsPfad"].InnerText;
                    SpieleListe[i].Kategorie = Spiel.Attributes["Kategorie"].InnerText;
                    SpieleListe[i].Publisher = Spiel.Attributes["Publisher"].InnerText;
                    SpieleListe[i].USK = Convert.ToInt32(Spiel.Attributes["Publisher"].InnerText);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
