using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static VerifyXunit.Verifier;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    public async Task TestStatementExampleLegacy()
    {
        Dictionary<string, Play> plays = new()
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") }
        };

        Invoice invoice = new(
            "BigCo",
            [
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            ]
        );

        var result = StatementPrinter.Print(invoice, plays);

        await Verify(result);
    }

    [Fact]
    public async Task TestTextStatementExample()
    {
        Dictionary<string, Play> plays = new()
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") },
            { "richard-iii", new Play("Richard III", 3718, "history") }
        };

        Invoice invoice = new(
            "BigCo",
            [
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
            ]
        );

        var result = StatementPrinter.Print(invoice, plays);

        await Verify(result);
    }
}
