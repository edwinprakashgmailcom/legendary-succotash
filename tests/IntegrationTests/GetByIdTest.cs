using Companies.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Companies.IntegrationTests;

[TestClass]
public class GetByIdTest
{
    [TestMethod]
    public async Task ReturnsItemGivenValidId()
    {
        var response = await ProgramTest.NewClient.GetAsync("companies/2");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        Assert.IsNotNull(stringResponse);
        var model = stringResponse.FromJson<Company>();

        Assert.AreEqual(2, model?.Id);
        Assert.AreEqual("Other", model?.Name);
        Assert.AreEqual("....is not", model?.Description);
    }

    [TestMethod]
    public async Task ReturnsNotFoundGivenInvalidId()
    {
        var response = await ProgramTest.NewClient.GetAsync("companies/3");

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
