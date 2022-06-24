﻿using Foundation.Interfaces;
using Foundation.Models;

namespace Foundation.AbstractClasses;

public abstract class AbstractIssueRepository : IIssueRepository
{
    public AbstractIssueRepository()
    {
    }

    public abstract string Type { get; }

    public abstract IEnumerable<IssueWorklogDto> WorklogsForDateRange(Integration integration, DateRange dateRange);
}