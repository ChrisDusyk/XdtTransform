using System;

namespace XdtTransform.Impl
{
	public interface IXdtTransformer
	{
		Boolean PreserveWhitespace { get; set; }

		/// <summary>
		/// Transforms the <paramref name="sourceXml" /> by applying the <paramref name="transformXml" /> xml and returns the result.
		/// </summary>
		/// <param name="sourceXml">The source XML.</param>
		/// <param name="transformXml">The transform XML.</param>
		/// <returns>The result.</returns>
		String Transform(String sourceXml, String transformXml);
	}
}