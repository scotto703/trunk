using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using OpenMetaverse;
using OpenMetaverse.StructuredData;

namespace BotGUI
{
    class BotActionWriter
    {
        //constructor that takes an UUID and Timer, event written is for
        //action 1 which is any animation part of the OpenMetavase Animation Library
        public static void WriteXml(UUID aUUID, int timer)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            using (XmlWriter writer = XmlWriter.Create("Event.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Event");
                writer.WriteElementString("Code", "1");
                writer.WriteElementString("UUID", aUUID.ToString());
                writer.WriteElementString("Counter", timer.ToString());
                writer.WriteEndElement();
                writer.Close();
            }
        }

        //constructor that takes an UUID and a Vector3, event written is for
        //action 2 which is the action to sit on a designated object
        public static void WriteXml(UUID aUUID, Vector3 vec)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            using (XmlWriter writer = XmlWriter.Create("Event.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Event");
                writer.WriteElementString("Code", "2");
                writer.WriteElementString("UUID", aUUID.ToString());
                writer.WriteElementString("Vector3", vec.ToString());
                writer.WriteEndElement();
                writer.Close();
            }
        }

        //constructor that takes a Vector3, event written for action 3
        //which is the action to look at a position 
        public static void WriteXml(Vector3 vec)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            using (XmlWriter writer = XmlWriter.Create("Event.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Event");
                writer.WriteElementString("Code", "3");
                writer.WriteElementString("Vector3", vec.ToString());
                writer.WriteEndElement();
                writer.Close();
            }
        }

        //constructor that takes nothing, event is written for action 4
        //which is for the action to stand up
        public static void WriteXml()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            using (XmlWriter writer = XmlWriter.Create("Event.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Event");
                writer.WriteElementString("Code", "4");
                writer.WriteEndElement();
                writer.Close();
            }
        }

        //constructor that takes and IventoryItem and an AttachmentPoint
        //event is written for action 5, which is the action to attach
        //an item to location on the bot
        public static void WriteXml(InventoryItem item, AttachmentPoint point)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            using (XmlWriter writer = XmlWriter.Create("Event.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Event");
                writer.WriteElementString("Code", "5");
                writer.WriteElementString("Item", item.ToString());
                writer.WriteElementString("Point", point.ToString());
                writer.WriteEndElement();
                writer.Close();
            }
        }
    }
}
