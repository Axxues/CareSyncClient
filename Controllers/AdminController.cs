using CareSync.Common;
using CareSync.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure this is at the top

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    // Add [FromQuery] to ensure it binds correctly from the URL
    public async Task<IActionResult> Index([FromQuery] PaginatedRequest request)
    {

        var page = request.PageNumber > 0 ? request.PageNumber : 1;
        var pageSize = PaginatedRequest.ITEMS_PER_PAGE;


        var safeSearchKeyword = request.SearchKeyword?.Trim();
        var safeSecondaryKeyword = request.SecondarySearchKeyword?.Trim();


        var totalPending = await _userManager.Users.CountAsync(u => u.EmailConfirmed == false);
        var totalActive = await _userManager.Users.CountAsync(u => u.EmailConfirmed == true);
        var totalAccounts = await _userManager.Users.CountAsync();

        var pendingQuery = _userManager.Users.Where(u => u.EmailConfirmed == false);


        if (!string.IsNullOrEmpty(safeSearchKeyword))
        {
            pendingQuery = pendingQuery.Where(u =>
                u.UserName.Contains(safeSearchKeyword ?? string.Empty) ||
                u.Email.Contains(safeSearchKeyword ?? string.Empty)
            );
        }


        var totalRecords = await pendingQuery.CountAsync();


        var pendingUsersList = await pendingQuery
            .OrderByDescending(u => u.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var displayList = new List<PendingUserViewModel>();
        foreach (var user in pendingUsersList)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            displayList.Add(new PendingUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = userRoles
            });
        }


        var paginatedResult = new PaginatedResult<PendingUserViewModel>
        {
            Page = page,
            TotalRecords = totalRecords,
            TotalCount = (int)Math.Ceiling(totalRecords / (double)pageSize),


            SearchKeyword = safeSearchKeyword,

            Result = displayList
        };

        var dashboardData = new AdminDashboardViewModel
        {
            TotalPendingAction = totalPending,
            TotalAccounts = totalAccounts,
            TotalActiveStaff = totalActive,
            PaginatedPendingUsers = paginatedResult
        };

        return View(dashboardData);
    }

    [HttpPost]
    public async Task<IActionResult> ActivateAccount(string userId)
    {
        // ... your existing ActivateAccount logic remains exactly the same ...
        if (string.IsNullOrEmpty(userId)) return NotFound();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        user.EmailConfirmed = true;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded) return RedirectToAction(nameof(Index));

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return RedirectToAction(nameof(Index));
    }
}