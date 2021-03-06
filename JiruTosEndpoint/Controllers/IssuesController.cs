using ClickUpService;
using Foundation;
using Foundation.Interfaces;
using Foundation.Models;
using JiraService;

namespace JiruTosEndpoint.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class IssuesController : ControllerBase
{
    private readonly IDatabase _db;
    private readonly IssueFascade _repo;

    public IssuesController(IDatabase db, IMapper mapper, ILogger<JiraIssueRepository> logger)
    {
        _db = db;
        _repo = new IssueFascade(new List<IIssueRepository>() {
            new JiraIssueRepository(logger, mapper), new ClickUpIssueRepository()
        });
    }

    [HttpPost]
    public ActionResult DateRangeWorklogs([FromBody] DateRange scanDate)
    {
        var worklogs = _repo.WorklogsForDateRange(_db.FindUser("ironoth12@gmail.com"), scanDate);
        return Ok(worklogs);
    }

    [HttpPost("{type}/{name}")]
    public ActionResult UpdateWorklog(string type, string name, [FromBody] UpdateWorklogModel model)
    {
        _repo.UpdateWorklog(_db.FindUser("ironoth12@gmail.com"), model, type, name);
        return Ok();
    }

    [HttpPost("{type}/{name}")]
    public ActionResult FilterIssues(string type, string name, [FromBody] Filter filter)
    {
        var issues = _repo.FilterIssuesByJql(_db.FindUser("ironoth12@gmail.com"), type, name, filter);
        return Ok(issues);
    }
}