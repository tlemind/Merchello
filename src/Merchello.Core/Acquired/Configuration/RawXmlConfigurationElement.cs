namespace Merchello.Core.Acquired.Configuration
{
    using System.Configuration;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// A configuration section that simply exposes the entire raw xml of the section itself which inheritors can use
    /// to do with as they please.
    /// </summary>
    /// UMBRACO_SRC
    internal abstract class RawXmlConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawXmlConfigurationElement"/> class.
        /// </summary>
        protected RawXmlConfigurationElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawXmlConfigurationElement"/> class.
        /// </summary>
        /// <param name="rawXml">
        /// The raw xml.
        /// </param>
        protected RawXmlConfigurationElement(XElement rawXml)
        {
            this.RawXml = rawXml;
        }

        /// <summary>
        /// Gets the raw xml.
        /// </summary>
        protected XElement RawXml { get; private set; }

        /// <summary>
        /// Deserializes the xml contents of an element.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="XmlReader"/>.
        /// </param>
        /// <param name="serializeCollectionKey">
        /// A value indicating whether or not to serialize the collection key. Not used.
        /// </param>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            this.RawXml = (XElement)XNode.ReadFrom(reader);
        }
    }
}