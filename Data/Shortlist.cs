using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;

namespace AllegroFinder.Data
{
    public class Shortlist : IXmlSerializable
    {
        private List<GameTitle> games = new List<GameTitle>();

        private void SetGameState(string title, bool check)
        {
            GameTitle temp = new GameTitle();
            temp.Title = title;
            GameTitle found = games.Find(delegate(GameTitle g) { return g.Equals(temp); });
            if (found != null)
            {
                found.Checked = check;
            }
        }

        public const string FileName = "shortlist.xml";

        public List<GameTitle> Games
        {
            get
            {
                return games;
            }
        }

        public int Count
        {
            get
            {
                return games.Count;
            }
        }

        public void Add(GameTitle title)
        {
            if (!games.Contains(title))
            {
                games.Add(title);
            }
        }

        public void Add(string title)
        {
            GameTitle temp = new GameTitle();
            temp.Title = title;
            Add(temp);
        }

        public void Delete(string title)
        {
            GameTitle temp = new GameTitle();
            temp.Title = title;
            games.Remove(temp);
        }

        public void SetChecked(string title)
        {
            SetGameState(title, true);
        }

        public void SetUnchecked(string title)
        {
            SetGameState(title, false);
        }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            games.Clear();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    GameTitle gt = new GameTitle();
                    gt.Checked = XmlConvert.ToBoolean(reader.GetAttribute("checked").ToLower());
                    gt.Title = reader.ReadString();
                    Add(gt);
                }
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (GameTitle title in games)
            {
                writer.WriteStartElement("title");
                writer.WriteAttributeString("checked", title.Checked.ToString());
                writer.WriteString(title.Title);
                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
