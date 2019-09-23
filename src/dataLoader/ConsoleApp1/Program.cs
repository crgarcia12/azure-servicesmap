
namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    class Program
    {
        static List<ServiceGroup> Groups = new List<ServiceGroup>();

        static async Task Main(string[] args)
        {
            string xmlContent = File.ReadAllText("C:\\gitrepos\\github\\crgarcia12\\azure-servicesmap\\data copy.xml");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            XmlNode current = doc.SelectSingleNode("ul/li");

            while (current != null)
            {
                ServiceGroup currentGroup = new ServiceGroup();
                ProcessLi(current, currentGroup);

                current = current.NextSibling;
            }
        }

        public static ServiceGroup ProcessLi(XmlNode node, ServiceGroup currentGroup)
        {
            int ariaLevel = 0;
            Int32.TryParse(node.Attributes != null ? node.Attributes["aria-level"]?.Value : null, out ariaLevel);


            if (ariaLevel == 1)
            {
                ServiceGroup newGroup = new ServiceGroup()
                {
                    Name = node.Attributes["title"].Value
                };
                Groups.Add(newGroup);
                return newGroup;
            }
            else if (ariaLevel == 2)
            {
                ProcessService(node, currentGroup);
            }
            return currentGroup;
        }

        public static void ProcessService(XmlNode node, ServiceGroup currentGroup)
        {
            string gNode = node.SelectSingleNode("a/div/svg/g")?.OuterXml;
            string nodeName = node.SelectSingleNode("a/div/div/@title")?.Value;


            if (!string.IsNullOrWhiteSpace(gNode) && !string.IsNullOrWhiteSpace(nodeName))
            {
                currentGroup.Services.Add(new Service
                {
                    GNode = gNode,
                    Name = nodeName
                });
            }
        }
    }
}

