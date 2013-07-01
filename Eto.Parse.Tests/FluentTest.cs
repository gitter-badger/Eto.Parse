using System;
using NUnit.Framework;
using Eto.Parse;

namespace Eto.Parse.Tests
{
	[TestFixture]
	public class FluentTest
	{
		[Test]
		public void TestFluent()
		{
			var input = "  hello (parsing world)  ";

			// repeating whitespace
			var ws = Terminals.WhiteSpace.Repeat();

			// parse a value with or without brackets
			var valueParser = Terminals.Set('(')
				.Then(Terminals.Set(')').Inverse().Repeat().Named("value"))
				.Then(Terminals.Set(')'))
				.Or(Terminals.WhiteSpace.Inverse().Repeat().Named("value"));

			// top level
			var parser =
				ws
				.Then(valueParser.Named("first"))
				.Then(ws)
				.Then(valueParser.Named("second"))
				.Then(ws)
				.Then(Terminals.End);

			var match = parser.Match(input);
			Assert.AreEqual("hello", match["first"]["value"].Value);
			Assert.AreEqual("parsing world", match["second"]["value"].Value);
		}
	}
}
