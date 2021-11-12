using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DocBook
{

	// XmlSerializer serializer = new XmlSerializer(typeof(Book));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (Book)serializer.Deserialize(reader);
	// }

	/// <summary> 법적 고지 </summary>
	[XmlRoot(ElementName = "legalnotice")]
	public class Legalnotice
	{

		[XmlElement(ElementName = "para")]
		public string Para { get; set; }
	}


	/// <summary> 소속 </summary>
	[XmlRoot(ElementName = "affiliation")]
	public class Affiliation
	{

		/// <summary> 소속 업체 </summary>
		[XmlElement(ElementName = "orgname")]
		public string Orgname { get; set; }


		/// <summary> 직책 </summary>
		[XmlElement(ElementName = "jobtitle")]
		public string Jobtitle { get; set; }
	}

	/// <summary> 저자 </summary>
	[XmlRoot(ElementName = "author")]
	public class Author
	{
		/// <summary> 이름(성) </summary>
		[XmlElement(ElementName = "firstname")]
		public string Firstname { get; set; }

		/// <summary> 이름(이름) </summary>
		[XmlElement(ElementName = "surname")]
		public string Surname { get; set; }

		/// <summary> 소속 </summary>
		[XmlElement(ElementName = "affiliation")]
		public Affiliation Affiliation { get; set; }
	}

	/// <summary> 책 정보 </summary>
	[XmlRoot(ElementName = "bookinfo")]
	public class Bookinfo
	{

		[XmlElement(ElementName = "legalnotice")]
		public Legalnotice Legalnotice { get; set; }

		[XmlElement(ElementName = "author")]
		public Author Author { get; set; }
	}

	/// <summary> 참고 문헌 </summary>
	[XmlRoot(ElementName = "epigraph")]
	public class Epigraph
	{

		[XmlElement(ElementName = "attribution")]
		public string Attribution { get; set; }

		[XmlElement(ElementName = "para")]
		public List<string> Para { get; set; }

		public Epigraph(string Attribution) 
		{

		}
	}

	[XmlRoot(ElementName = "abstract")]
	public class Abstract
	{

		[XmlElement(ElementName = "para")]
		public List<string> Para { get; set; }
	}

	[XmlRoot(ElementName = "dedication")]
	public class Dedication
	{

		[XmlElement(ElementName = "para")]
		public List<string> Para { get; set; }
	}

	[XmlRoot(ElementName = "preface")]
	public class Preface
	{

		[XmlElement(ElementName = "title")]
		public string Title { get; set; }

		[XmlElement(ElementName = "para")]
		public List<string> Para { get; set; }
	}

	[XmlRoot(ElementName = "section")]
	public class Section
	{

		[XmlElement(ElementName = "title")]
		public string Title { get; set; }

		[XmlElement(ElementName = "para")]
		public List<string> Para { get; set; }
	}


	[XmlRoot(ElementName = "chapter")]
	public class Chapter
	{

		[XmlElement(ElementName = "title")]
		public string Title { get; set; }

		[XmlElement(ElementName = "para")]
		public List<string> Para { get; set; }

		[XmlElement(ElementName = "section")]
		public List<Section> Section { get; set; }
	}

	/// <summary> 부록 </summary>
	[XmlRoot(ElementName = "appendix")]
	public class Appendix
	{
		/// <summary> 부록 제목 </summary>
		[XmlElement(ElementName = "title")]
		public string Title { get; set; }

		/// <summary> 부록 내용 </summary>
		[XmlElement(ElementName = "para")]
		public List<string> Para { get; set; }
	}

	[XmlRoot(ElementName = "book")]
	public class Book
	{

		[XmlElement(ElementName = "title")]
		public string Title { get; set; }

		[XmlElement(ElementName = "titleabbrev")]
		public string Titleabbrev { get; set; }

		[XmlElement(ElementName = "bookinfo")]
		public Bookinfo Bookinfo { get; set; }

		[XmlElement(ElementName = "epigraph")]
		public Epigraph Epigraph { get; set; }

		[XmlElement(ElementName = "abstract")]
		public Abstract Abstract { get; set; }

		[XmlElement(ElementName = "dedication")]
		public Dedication Dedication { get; set; }

		[XmlElement(ElementName = "preface")]
		public Preface Preface { get; set; }

		[XmlElement(ElementName = "chapter")]
		public Chapter Chapter { get; set; }

		[XmlElement(ElementName = "appendix")]
		public Appendix Appendix { get; set; }

		[XmlAttribute(AttributeName = "version")]
		public double Version { get; set; }

		[XmlAttribute(AttributeName = "encoding")]
		public string Encoding { get; set; }

		[XmlAttribute(AttributeName = "lang")]
		public string Lang { get; set; }

		[XmlText]
		public string Text { get; set; }
	}


}
