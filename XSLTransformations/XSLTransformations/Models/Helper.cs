using Saxon.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

namespace XSLTransformations.Models
{
    public class Helper
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string SerializeToString(object obj)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
        }

        public static string ApplyXSLT(string xml, string xsltFile)
        {
            XslCompiledTransform xt = new XslCompiledTransform();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XsltSettings settings = new XsltSettings(false, true);
            xt.Load(xsltFile, settings, null);
            StringBuilder output = new StringBuilder();

            using (StringWriter xmlwriter = new StringWriter(output))
            {
                xt.Transform(doc, null, xmlwriter);
            }

            return output.ToString();
        }

        public static string TransformUsingSaxon(string xml, string xsltFile)
        {
            XslCompiledTransform xt = new XslCompiledTransform();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            var xslt = new FileInfo(xsltFile);
            Processor processor = new Processor();
            XsltCompiler compiler = processor.NewXsltCompiler();
            XsltExecutable executable = compiler.Compile(new Uri(xslt.FullName));
            XsltTransformer transformer = executable.Load();
            XdmNode input = processor.NewDocumentBuilder().Build(doc);
            transformer.InitialContextNode = input;
            Serializer serializer = new Serializer();

            StringBuilder output = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(output))
            {
                serializer.SetOutputWriter(stringWriter);
                transformer.Run(serializer);
                return output.ToString();
            }
        }

        private static XsltTransformer InitializeSaxonProcessor(string xsltfile, Processor saxprocess)
        {
            XsltCompiler saxXsltCompiler = saxprocess.NewXsltCompiler();
            using (StreamReader sr = new StreamReader(xsltfile))
            {
                XsltExecutable xsltexec = saxXsltCompiler.Compile(sr);
                XsltTransformer xslttrans = xsltexec.Load();
                return xslttrans;
            }
        }
    }
}