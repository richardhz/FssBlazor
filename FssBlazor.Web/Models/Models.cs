namespace FssBlazor.Web.Models;

public class FileItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long Size { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string FolderId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public FileStatus Status { get; set; }
    public bool IsShared { get; set; }
    public string? DownloadUrl { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public int DownloadCount { get; set; }
    public string FileHash { get; set; } = string.Empty;
    public long Version { get; set; } = 1;
}

public enum FileStatus
{
    Uploading,
    Available,
    Processing,
    Error,
    Expired,
    Deleted
}

public class FolderItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ParentId { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public int FileCount { get; set; }
    public int SubFolderCount { get; set; }
    public bool IsShared { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class UserProfile
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public DateTime LastLoginDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string Department { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
}

public class SharePermission
{
    public string Id { get; set; } = string.Empty;
    public string FileId { get; set; } = string.Empty;
    public string FolderId { get; set; } = string.Empty;
    public string SharedWithUserId { get; set; } = string.Empty;
    public string SharedWithEmail { get; set; } = string.Empty;
    public ShareType ShareType { get; set; }
    public PermissionLevel Permission { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string ShareToken { get; set; } = string.Empty;
}

public enum ShareType
{
    Internal,
    External,
    Public,
    Organization
}

public enum PermissionLevel
{
    View,
    Download,
    Edit,
    Admin
}

public class UploadProgress
{
    public string Id { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long TotalBytes { get; set; }
    public long UploadedBytes { get; set; }
    public double PercentageComplete => TotalBytes > 0 ? (double)UploadedBytes / TotalBytes * 100 : 0;
    public UploadStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? CompletedTime { get; set; }
    public TimeSpan? EstimatedTimeRemaining { get; set; }
    public double UploadSpeed { get; set; } // bytes per second
}

public enum UploadStatus
{
    Pending,
    Uploading,
    Processing,
    Completed,
    Failed,
    Cancelled
}

public class DownloadLink
{
    public string Id { get; set; } = string.Empty;
    public string FileId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int MaxDownloads { get; set; }
    public int DownloadCount { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool RequireAuth { get; set; } = true;
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
    public int StatusCode { get; set; }
}

public class ApiResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
    public int StatusCode { get; set; }
}

public class FileBrowserRequest
{
    public string FolderId { get; set; } = string.Empty;
    public string SearchTerm { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string SortBy { get; set; } = "Name";
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
    public List<string> FileTypes { get; set; } = new();
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public enum SortDirection
{
    Ascending,
    Descending
}

public class FileBrowserResponse
{
    public List<FolderItem> Folders { get; set; } = new();
    public List<FileItem> Files { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage => PageNumber * PageSize < TotalCount;
    public bool HasPreviousPage => PageNumber > 1;
}

public class DashboardStats
{
    public int TotalFiles { get; set; }
    public int TotalFolders { get; set; }
    public long TotalStorageUsed { get; set; }
    public int FilesSharedWithMe { get; set; }
    public int FilesIShared { get; set; }
    public int RecentUploads { get; set; }
    public int RecentDownloads { get; set; }
    public List<FileItem> RecentFiles { get; set; } = new();
    public List<FileItem> PopularFiles { get; set; } = new();
}

public class NotificationMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; }
    public string? ActionUrl { get; set; }
    public string? ActionText { get; set; }
}

public enum NotificationType
{
    Success,
    Information,
    Warning,
    Error
}