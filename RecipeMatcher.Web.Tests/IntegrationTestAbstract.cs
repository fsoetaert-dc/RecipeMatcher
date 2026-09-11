namespace RecipeMatcher.Web.Tests;

// MME: Match filename with classname
public abstract class IntegrationTest : IDisposable
{
    private readonly CustomWebApplicationFactory factory = new();

    protected HttpClient Client { get; }
    protected EfReader Reader { get; }

    protected EfWriter Writer { get; }

    protected IntegrationTest()
    {
        Client = factory.CreateClient();
        Reader = factory.GetReader();
        Writer = factory.GetWriter();

        factory.ResetDatabase();
    }

    public void Dispose()
    {
        Client.Dispose();
        factory.Dispose();
    }
}