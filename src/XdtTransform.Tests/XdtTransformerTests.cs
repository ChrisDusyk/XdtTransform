using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XdtTransform.Impl;

namespace XdtTransform.Tests
{
	[TestClass]
	public class XdtTransformerTests
	{
		private const String GenericSourceXml = @"<?xml version=""1.0""?>
<configuration>
</configuration>";

		private const String GenericTransformXml = @"<?xml version=""1.0""?>
<configuration xmlns:xdt=""http://schemas.microsoft.com/XML-Document-Transform"">
</configuration>";

		#region Parameter Checking

		[TestCategory("Parameter Checking"), TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Should_not_accept_a_null_source_parameter()
		{
			// Arrange

			// Act

			// Assert
			XdtTransformer.Current.Transform(null, GenericTransformXml);
		}

		[TestCategory("Parameter Checking"), TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Should_not_accept_an_empty_source_parameter()
		{
			// Arrange

			// Act

			// Assert
			XdtTransformer.Current.Transform(String.Empty, GenericTransformXml);
		}

		[TestCategory("Parameter Checking"), TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Should_not_accept_a_null_transform_parameter()
		{
			// Arrange

			// Act

			// Assert
			XdtTransformer.Current.Transform(GenericSourceXml, null);
		}

		[TestCategory("Parameter Checking"), TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Should_not_accept_an_empty_transform_parameter()
		{
			XdtTransformer.Current.Transform(GenericSourceXml, String.Empty);
		}

		#endregion Parameter Checking

		#region Simple Usage

		[TestCategory("Simple Usage"), TestMethod]
		public void Should_handle_simple_transform()
		{
			// Arrange
			var source = GenericSourceXml;
			var transform = GenericTransformXml;
			var expected = GenericSourceXml;

			// Act
			var actual = XdtTransformer.Current.Transform(source, transform);

			// Assert
			Assert.IsTrue(!String.IsNullOrEmpty(actual), "Should not return null or empty value.");
			Assert.AreEqual(expected, actual, "Actual did not match expected result.");
		}

		#endregion Simple Usage
	}
}