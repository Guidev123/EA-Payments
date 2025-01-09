﻿using System.Text.Json.Serialization;

namespace Payments.Application.Responses;

public class PagedResponse<TData> : Response<TData>
{
    [JsonConstructor]
    public PagedResponse(
        int totalCount,
        TData? data = default,
        int currentPage = 1,
        int pageSize = 5,
        int code = 200,
        string? message = null,
        string[]? errors = null)
        : base(data, code, message, errors)
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public PagedResponse(
        TData? data,
        int code = 200,
        string? message = null,
        string[]? errors = null)
        : base(data, code, message, errors)
    {
    }

    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = 5;
    public int TotalCount { get; set; }
}