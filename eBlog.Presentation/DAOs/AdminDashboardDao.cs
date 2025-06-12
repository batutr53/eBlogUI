using Dapper;
using eBlog.Domain.Interfaces.DAO;
using eBlog.Domain.Models;
using eBlog.Domain.Models.Dashboard;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class AdminDashboardDao : IAdminDashboardDao
{
    private readonly IConfiguration _configuration;

    public AdminDashboardDao(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    private NpgsqlConnection CreateConnection()
    => new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));

    public async Task<List<TopLikedPost>> GetTopLikedPostsAsync()
    {
        const string sql = @"
            SELECT 
                p.""Id"" AS ""PostId"",
                p.""Title"",
                u.""UserName"" AS ""Author"",
                COUNT(l.""Id"") AS ""LikeCount"",
                p.""CreatedAt""
            FROM ""Posts"" p
            LEFT JOIN ""Likes"" l ON l.""PostId"" = p.""Id""
            LEFT JOIN ""Users"" u ON u.""Id"" = p.""AuthorId""
            GROUP BY p.""Id"", p.""Title"", u.""UserName"", p.""CreatedAt""
            ORDER BY COUNT(l.""Id"") DESC
            LIMIT 10;
        ";

        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var result = await conn.QueryAsync<TopLikedPost>(sql);
        return result.ToList();
    }
    public async Task<DashboardTotals> GetDashboardTotalsAsync()
    {
        const string sql = @"
        SELECT
            (SELECT COUNT(*) FROM ""Users"") AS ""UserCount"",
            (SELECT COUNT(*) FROM ""Posts"") AS ""PostCount"",
            (SELECT COUNT(*) FROM ""Comments"") AS ""CommentCount"",
            (SELECT COUNT(*) FROM ""Products"") AS ""ProductCount"",
            (SELECT COUNT(*) FROM ""ProductOrders"") AS ""OrderCount"",
            (SELECT COALESCE(SUM(""TotalPrice""), 0) FROM ""ProductOrders"") AS ""TotalRevenue""
    ";

        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        return await conn.QuerySingleAsync<DashboardTotals>(sql);
    }
    public async Task<List<TopSellingProduct>> GetTopSellingProductsAsync()
    {
        const string sql = @"
        SELECT
            p.""Id"" AS ""ProductId"",
            p.""Name"" AS ""ProductName"",
            SUM(po.""Quantity"") AS ""TotalQuantitySold"",
            SUM(po.""TotalPrice"") AS ""TotalRevenue""
        FROM ""ProductOrders"" po
        INNER JOIN ""Products"" p ON p.""Id"" = po.""ProductId""
        GROUP BY p.""Id"", p.""Name""
        ORDER BY SUM(po.""Quantity"") DESC
        LIMIT 10;
    ";

        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var result = await conn.QueryAsync<TopSellingProduct>(sql);
        return result.ToList();
    }
    public async Task<List<TopCommentedPost>> GetTopCommentedPostsAsync()
    {
        const string sql = @"
        SELECT 
            p.""Id"" AS ""PostId"",
            p.""Title"",
            u.""UserName"" AS ""Author"",
            COUNT(c.""Id"") AS ""CommentCount"",
            p.""CreatedAt""
        FROM ""Posts"" p
        LEFT JOIN ""Comments"" c ON c.""PostId"" = p.""Id""
        LEFT JOIN ""Users"" u ON u.""Id"" = p.""AuthorId""
        GROUP BY p.""Id"", p.""Title"", u.""UserName"", p.""CreatedAt""
        ORDER BY COUNT(c.""Id"") DESC
        LIMIT 10;
    ";

        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var result = await conn.QueryAsync<TopCommentedPost>(sql);
        return result.ToList();
    }
    public async Task<List<TopBuyer>> GetTopBuyersAsync()
    {
        const string sql = @"
        SELECT 
            u.""Id"" AS ""UserId"",
            CONCAT(u.""FirstName"", ' ', u.""LastName"") AS ""FullName"",
            u.""Email"",
            COUNT(o.""Id"") AS ""TotalOrders"",
            COALESCE(SUM(o.""TotalPrice""), 0) AS ""TotalSpent""
        FROM ""Users"" u
        INNER JOIN ""ProductOrders"" o ON o.""BuyerId"" = u.""Id""
        GROUP BY u.""Id"", u.""FirstName"", u.""LastName"", u.""Email""
        ORDER BY COUNT(o.""Id"") DESC
        LIMIT 10;
    ";

        using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var buyers = await connection.QueryAsync<TopBuyer>(sql);
        return buyers.ToList();
    }
    public async Task<List<TopRatedProduct>> GetTopRatedProductsAsync()
    {
        const string sql = @"
        SELECT 
            p.""Id"" AS ""ProductId"",
            p.""Name"" AS ""ProductName"",
            ROUND(AVG(r.""Rating""), 2) AS ""AverageRating"",
            COUNT(r.""Id"") AS ""TotalReviews""
        FROM ""Products"" p
        INNER JOIN ""ProductRatings"" r ON r.""ProductId"" = p.""Id""
        GROUP BY p.""Id"", p.""Name""
        HAVING COUNT(r.""Id"") > 0
        ORDER BY AVG(r.""Rating"") DESC
        LIMIT 10;
    ";

        using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var result = await connection.QueryAsync<TopRatedProduct>(sql);
        return result.ToList();
    }
    public async Task<List<OrderStatusCount>> GetOrderStatusCountsAsync()
    {
        const string sql = @"
        SELECT 
            o.""Status"",
            COUNT(*) AS ""Count""
        FROM ""ProductOrders"" o
        GROUP BY o.""Status"";
    ";
        using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var result = await connection.QueryAsync<OrderStatusCount>(sql);
        return result.ToList();
    }
    public async Task<List<UserGrowthStat>> GetUserGrowthAsync(int days)
    {
        const string sql = @"
        SELECT 
            DATE_TRUNC('day', u.""RegisteredAt"") AS ""Date"",
            COUNT(*) AS ""Count""
        FROM ""Users"" u
        WHERE u.""RegisteredAt"" >= CURRENT_DATE - @Days
        GROUP BY DATE_TRUNC('day', u.""RegisteredAt"")
        ORDER BY ""Date"";
    ";
        using var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var result = await conn.QueryAsync<UserGrowthStat>(sql, new { Days = days });
        return result.ToList();
    }
    public async Task<List<CategoryDistribution>> GetCategoryDistributionAsync()
    {
        var sql = @"
            SELECT c.""Name"" AS ""CategoryName"", COUNT(p.""Id"") AS ""PostCount""
            FROM ""Categories"" c
            LEFT JOIN ""Posts"" p ON c.""Id"" = p.""CategoryId""
            GROUP BY c.""Name"";";
        using var conn = CreateConnection();
        var result = await conn.QueryAsync<CategoryDistribution>(sql);
        return result.ToList();
    }

    public async Task<List<ActiveAuthor>> GetActiveAuthorsAsync()
    {
        var sql = @"
            SELECT u.""Id"" AS ""UserId"", u.""UserName"", COUNT(p.""Id"") AS ""PostCount""
            FROM ""Users"" u
            JOIN ""Posts"" p ON u.""Id"" = p.""AuthorId""
            GROUP BY u.""Id"", u.""UserName""
            ORDER BY COUNT(p.""Id"") DESC
            LIMIT 5;";
        using var conn = CreateConnection();
        return (await conn.QueryAsync<ActiveAuthor>(sql)).ToList();
    }

    public async Task<List<PostModuleUsage>> GetPostModuleUsageAsync()
    {
        var sql = @"
            SELECT ""Type"" AS ""ModuleType"", COUNT(*) AS ""UsageCount""
            FROM ""PostModules""
            GROUP BY ""Type""
            ORDER BY ""UsageCount"" DESC;";
        using var conn = CreateConnection();
        return (await conn.QueryAsync<PostModuleUsage>(sql)).ToList();
    }

    public async Task<List<CouponUsage>> GetCouponUsageAsync()
    {
        var sql = @"
            SELECT c.""Code"" AS ""CouponCode"", COUNT(po.""Id"") AS ""UsageCount""
            FROM ""Coupons"" c
            JOIN ""ProductOrders"" po ON po.""CouponId"" = c.""Id""
            GROUP BY c.""Code""
            ORDER BY ""UsageCount"" DESC;";
        using var conn = CreateConnection();
        return (await conn.QueryAsync<CouponUsage>(sql)).ToList();
    }

    public async Task<List<LoginActivity>> GetRecentLoginActivitiesAsync()
    {
        var sql = @"
            SELECT u.""Email"", lt.""IpAddress"", lt.""LoginTime""
            FROM ""LoginTracks"" lt
            JOIN ""Users"" u ON lt.""UserId"" = u.""Id""
            ORDER BY lt.""LoginTime"" DESC
            LIMIT 10;";
        using var conn = CreateConnection();
        return (await conn.QueryAsync<LoginActivity>(sql)).ToList();
    }

    public async Task<List<ErrorLogCount>> GetErrorLogCountsAsync(int days)
    {
        var sql = @"
            SELECT DATE_TRUNC('day', ""LoggedAt"") AS ""Date"", COUNT(*) AS ""Count""
            FROM ""Logs""
            WHERE ""Level"" = 'Error' AND ""LoggedAt"" >= CURRENT_DATE - @Days
            GROUP BY DATE_TRUNC('day', ""LoggedAt"")
            ORDER BY ""Date"";";
        using var conn = CreateConnection();
        return (await conn.QueryAsync<ErrorLogCount>(sql, new { Days = days })).ToList();
    }

    public async Task<List<HourlyTraffic>> GetHourlyTrafficAsync()
    {
        var sql = @"
            SELECT EXTRACT(HOUR FROM ""VisitedAt"")::int AS ""Hour"", COUNT(*) AS ""VisitCount""
            FROM ""TrafficLogs""
            GROUP BY ""Hour""
            ORDER BY ""Hour"";";
        using var conn = CreateConnection();
        return (await conn.QueryAsync<HourlyTraffic>(sql)).ToList();
    }

    public async Task<List<TagUsage>> GetTagUsageStatsAsync()
    {
        var sql = @"
            SELECT t.""Name"" AS ""TagName"", COUNT(*) AS ""Count""
            FROM ""PostTags"" pt
            JOIN ""Tags"" t ON pt.""TagId"" = t.""Id""
            GROUP BY t.""Name""
            ORDER BY ""Count"" DESC;";
        using var conn = CreateConnection();
        return (await conn.QueryAsync<TagUsage>(sql)).ToList();
    }

    public async Task<PersonalSummary> GetPersonalSummaryAsync(Guid userId)
    {
        var sql = @"
            SELECT 
                (SELECT COUNT(*) FROM ""Posts"" WHERE ""AuthorId"" = @UserId) AS ""TotalPosts"",
                (SELECT COALESCE(SUM(""LikeCount""), 0) FROM ""Posts"" WHERE ""AuthorId"" = @UserId) AS ""TotalLikes"",
                (SELECT COUNT(*) FROM ""Comments"" WHERE ""UserId"" = @UserId) AS ""TotalComments"",
                (SELECT MAX(""LastLogin"") FROM ""Users"" WHERE ""Id"" = @UserId) AS ""LastActive"";";
        using var conn = CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalSummary>(sql, new { UserId = userId });
    }


}
