using FssBlazor.Web.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace FssBlazor.Web.Services;

public interface IFileService
{
    Task<ApiResponse<FileBrowserResponse>> GetFilesAndFoldersAsync(FileBrowserRequest request);
    Task<ApiResponse<FileItem>> GetFileAsync(string fileId);
    Task<ApiResponse<FolderItem>> GetFolderAsync(string folderId);
    Task<ApiResponse<List<FolderItem>>> GetBreadcrumbAsync(string folderId);
    Task<ApiResponse<FileItem>> UploadFileAsync(IBrowserFile file, string folderId, string description = "");
    Task<ApiResponse> DeleteFileAsync(string fileId);
    Task<ApiResponse> DeleteFolderAsync(string folderId);
    Task<ApiResponse<FolderItem>> CreateFolderAsync(string parentId, string name, string description = "");
    Task<ApiResponse<FileItem>> RenameFileAsync(string fileId, string newName);
    Task<ApiResponse<FolderItem>> RenameFolderAsync(string folderId, string newName);
    Task<ApiResponse<FileItem>> MoveFileAsync(string fileId, string newFolderId);
    Task<ApiResponse<FolderItem>> MoveFolderAsync(string folderId, string newParentId);
    Task<ApiResponse<DownloadLink>> GenerateDownloadLinkAsync(string fileId, DateTime? expiryDate = null, int maxDownloads = 0);
    Task<ApiResponse<List<FileItem>>> SearchFilesAsync(string searchTerm, int pageNumber = 1, int pageSize = 20);
    Task<ApiResponse<DashboardStats>> GetDashboardStatsAsync();
    Task<ApiResponse<List<FileItem>>> GetRecentFilesAsync(int count = 10);
    Task<ApiResponse<List<FileItem>>> GetSharedFilesAsync();
    Task<ApiResponse<List<FileItem>>> GetMyFilesAsync();
}

public interface IAuthService
{
    Task<bool> IsAuthenticatedAsync();
    Task<UserProfile?> GetCurrentUserAsync();
    Task<ApiResponse> SignInAsync();
    Task<ApiResponse> SignOutAsync();
    Task<string?> GetAccessTokenAsync();
    Task<bool> HasPermissionAsync(string permission);
    Task<bool> IsInRoleAsync(string role);
    event Action<UserProfile?> UserChanged;
}

public interface INotificationService
{
    event Action<NotificationMessage> NotificationReceived;
    Task ShowSuccessAsync(string message, string? title = null);
    Task ShowErrorAsync(string message, string? title = null);
    Task ShowWarningAsync(string message, string? title = null);
    Task ShowInfoAsync(string message, string? title = null);
    Task<List<NotificationMessage>> GetNotificationsAsync();
    Task MarkAsReadAsync(string notificationId);
    Task ClearAllAsync();
}

public interface IDownloadService
{
    Task<ApiResponse<DownloadLink>> StartDownloadAsync(string fileId);
    Task<ApiResponse<Stream>> DownloadFileStreamAsync(string downloadLinkId);
    Task<ApiResponse<byte[]>> DownloadFileAsync(string downloadLinkId);
    Task<ApiResponse<List<DownloadLink>>> GetDownloadHistoryAsync();
    Task<ApiResponse> CancelDownloadAsync(string downloadLinkId);
    event Action<string, double> DownloadProgressChanged;
}

public interface IShareService
{
    Task<ApiResponse<List<SharePermission>>> GetFileSharesAsync(string fileId);
    Task<ApiResponse<List<SharePermission>>> GetFolderSharesAsync(string folderId);
    Task<ApiResponse<SharePermission>> ShareFileAsync(string fileId, string shareWithEmail, PermissionLevel permission, DateTime? expiryDate = null);
    Task<ApiResponse<SharePermission>> ShareFolderAsync(string folderId, string shareWithEmail, PermissionLevel permission, DateTime? expiryDate = null);
    Task<ApiResponse> UpdateSharePermissionAsync(string shareId, PermissionLevel permission, DateTime? expiryDate = null);
    Task<ApiResponse> RevokeShareAsync(string shareId);
    Task<ApiResponse<List<FileItem>>> GetFilesSharedWithMeAsync();
    Task<ApiResponse<List<FileItem>>> GetFilesISharedAsync();
    Task<ApiResponse<SharePermission>> CreatePublicShareAsync(string fileId, DateTime? expiryDate = null, int maxDownloads = 0);
}

public interface IUploadService
{
    Task<ApiResponse<string>> StartUploadAsync(string fileName, long fileSize, string folderId, string contentType);
    Task<ApiResponse> UploadChunkAsync(string uploadId, byte[] chunk, int chunkNumber, bool isLastChunk);
    Task<ApiResponse<FileItem>> CompleteUploadAsync(string uploadId, string description = "");
    Task<ApiResponse> CancelUploadAsync(string uploadId);
    Task<List<UploadProgress>> GetActiveUploadsAsync();
    event Action<UploadProgress> UploadProgressChanged;
}