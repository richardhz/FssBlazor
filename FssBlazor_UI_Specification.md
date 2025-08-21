# File Share Service (FSS) Blazor UI Specification

## 1. Project Overview

### 1.1 Purpose
This specification outlines the development of a Blazor Server application for a File Share Service (FSS) UI, inspired by the UKHO file-share-service-ui repository. The application will provide a modern web interface for file sharing operations with secure authentication and comprehensive file management capabilities.

### 1.2 Technology Stack
- **Framework**: Blazor Server (.NET 9)
- **Architecture**: .NET Aspire with service defaults
- **Caching**: Redis Output Caching
- **Styling**: UKHO Admiralty Design System with custom components
- **Authentication**: Azure Active Directory B2C / Identity Server
- **API Communication**: HTTP Client with typed clients
- **File Handling**: Multipart uploads with progress tracking

### 1.3 Current Workspace Structure
```
FssBlazor/
??? FssBlazor.Web/              # Main Blazor Server application
??? FssBlazor.ApiService/       # Backend API service
??? FssBlazor.AppHost/          # Aspire orchestration
??? FssBlazor.ServiceDefaults/  # Common service configurations
```

## 2. Design System Integration

### 2.1 UKHO Admiralty Design System
The application will utilize the **UKHO Admiralty Design System** for consistent styling and components:

- **Package**: `@ukho/admiralty-design-system`
- **Framework**: Component-based design system
- **Benefits**: 
  - Government Digital Service (GDS) compliant
  - Maritime-focused design patterns
  - Accessibility built-in (WCAG 2.1 AA)
  - Consistent with UKHO brand guidelines

### 2.2 Integration Approach
```
wwwroot/
??? css/
?   ??? admiralty-design-system.css    # Main ADS stylesheet
?   ??? site.css                       # Custom overrides
?   ??? components/                    # Component-specific styles
??? js/
?   ??? admiralty-design-system.js     # ADS JavaScript components
?   ??? site.js                        # Custom JavaScript
??? images/
    ??? ukho/                          # UKHO brand assets
```

### 2.3 Component Mapping
| Blazor Component | ADS Component | Purpose |
|------------------|---------------|---------|
| MainLayout | `ads-page-template` | Primary page structure |
| NavMenu | `ads-navigation` | Main navigation |
| Button | `ads-button` | Actions and interactions |
| Form Controls | `ads-input`, `ads-select` | Form elements |
| Notifications | `ads-notification-banner` | Success/error messages |
| Tables | `ads-table` | File listings |
| Cards | `ads-card` | File information display |
| Modals | `ads-modal` | Dialogs and confirmations |

## 2.2 File Management Components

#### 2.2.1 File Upload Component
```
Components/FileManagement/FileUpload.razor
??? Features:
?   ??? ADS-styled drag & drop interface
?   ??? Multiple file selection with ADS file input
?   ??? Progress tracking with ADS progress bars
?   ??? File validation with ADS error messages
?   ??? Metadata input forms using ADS form components
?   ??? Upload queue management with ADS tables
```

#### 2.2.2 File Browser Component
```
Components/FileManagement/FileBrowser.razor
??? Features:
?   ??? Hierarchical folder navigation with ADS breadcrumbs
?   ??? File listing with ADS tables and sorting
?   ??? Search functionality with ADS search component
?   ??? File preview with ADS modals
?   ??? Bulk operations with ADS checkboxes and action bars
?   ??? Pagination with ADS pagination component
```

#### 2.2.3 File Details Component
```
Components/FileManagement/FileDetails.razor
??? Features:
?   ??? File metadata display with ADS summary cards
?   ??? Download links with ADS button styles
?   ??? Sharing permissions with ADS form controls
?   ??? Version history with ADS timeline component
?   ??? File operations with ADS action menus
```

### 2.3 User Interface Components

#### 2.3.1 Navigation Structure
```
Components/Layout/
??? MainLayout.razor           # ADS page template integration
??? NavMenu.razor             # ADS navigation component
??? TopBar.razor              # ADS header with crown and logo
??? Sidebar.razor             # ADS side navigation
??? Footer.razor              # ADS footer with government links
```

#### 2.3.2 Shared Components
```
Components/Shared/
??? LoadingSpinner.razor      # ADS loading indicators
??? ConfirmDialog.razor       # ADS modal confirmations
??? NotificationToast.razor   # ADS notification banners
??? ProgressBar.razor         # ADS progress components
??? SearchBox.razor           # ADS search component
??? BreadcrumbNav.razor       # ADS breadcrumb navigation
```

## 3. Page Structure & Routes

### 3.1 Main Pages
```
Components/Pages/
??? Home.razor                # Dashboard/landing page
??? Browse.razor              # File browser interface
??? Upload.razor              # File upload interface
??? SharedFiles.razor         # Files shared with user
??? MyFiles.razor             # User's private files
??? Downloads.razor           # Download history/queue
??? Settings.razor            # User preferences
??? Admin/                    # Administrative pages
?   ??? UserManagement.razor
?   ??? SystemSettings.razor
?   ??? Analytics.razor
??? Error.razor               # Error handling page
```

### 3.2 Route Configuration
| Route | Component | Description |
|-------|-----------|-------------|
| `/` | Home.razor | Dashboard with recent activity |
| `/browse` | Browse.razor | Main file browser |
| `/upload` | Upload.razor | File upload interface |
| `/shared` | SharedFiles.razor | Files shared with user |
| `/myfiles` | MyFiles.razor | User's private files |
| `/downloads` | Downloads.razor | Download management |
| `/settings` | Settings.razor | User settings |
| `/admin/users` | UserManagement.razor | Admin user management |
| `/admin/settings` | SystemSettings.razor | System configuration |

## 4. Service Layer Architecture

### 4.1 HTTP Clients & API Services
```
Services/
??? IFileService.cs           # File operations interface
??? FileService.cs            # File operations implementation
??? IAuthService.cs           # Authentication interface
??? AuthService.cs            # Authentication implementation
??? INotificationService.cs   # Notifications interface
??? NotificationService.cs    # Notifications implementation
??? IDownloadService.cs       # Download management interface
??? DownloadService.cs        # Download management implementation
```

### 4.2 Data Models
```
Models/
??? FileItem.cs               # File metadata model
??? FolderItem.cs             # Folder model
??? UserProfile.cs            # User information model
??? SharePermission.cs        # Sharing permissions model
??? UploadProgress.cs         # Upload tracking model
??? DownloadLink.cs           # Download link model
??? ApiResponse.cs            # Generic API response wrapper
```

## 5. Security & Compliance

### 5.1 Authentication Requirements
- Azure Active Directory B2C integration
- Multi-factor authentication support
- Session management with timeout
- Secure token storage and refresh

### 5.2 Authorization Model
- Role-based access control (Admin, User, ReadOnly)
- Resource-level permissions
- File sharing permission management
- Audit logging for security events

### 5.3 Data Protection
- HTTPS enforcement
- CSRF protection via Antiforgery tokens
- XSS protection with Content Security Policy
- File upload validation and sanitization
- Secure download link generation with expiration

## 6. Technical Implementation Details

### 6.1 Admiralty Design System Setup
```html
<!-- _Host.cshtml or App.razor head section -->
<link href="~/css/admiralty-design-system.css" rel="stylesheet" />
<link href="~/css/site.css" rel="stylesheet" />
<script src="~/js/admiralty-design-system.js"></script>
```

### 6.2 Blazor Configuration Updates
```csharp
// Program.cs additions
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IDownloadService, DownloadService>();

// Authentication
builder.Services.AddAuthentication("AzureADB2C")
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("FileAccess", policy => policy.RequireAuthenticatedUser());
});

// File upload configuration
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1024 * 1024 * 100; // 100MB
});
```

### 6.3 API Client Configuration
```csharp
builder.Services.AddHttpClient<IFileService, FileService>(client =>
{
    client.BaseAddress = new("https+http://apiservice");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
```

### 6.4 State Management
- Use Blazor's built-in state management for component state
- Implement service-level state for cross-component data
- Utilize browser local storage for user preferences
- Implement real-time updates with SignalR for file operations

## 7. UI/UX Design Guidelines

### 7.1 Design Principles
- **Government Digital Service (GDS) Standards**: Following GDS design principles
- **Admiralty Brand Compliance**: Consistent with UKHO visual identity
- **Accessibility**: WCAG 2.1 AA compliance built into ADS components
- **Performance**: Lazy loading, virtualization for large lists
- **User Experience**: Maritime-focused intuitive navigation

### 7.2 Visual Components
- UKHO crown logo and government branding
- Admiralty blue color scheme with accessible contrast ratios
- Loading states using ADS spinner components
- Toast notifications using ADS notification banners
- Modal dialogs using ADS modal components
- Progress indicators using ADS progress bars

### 7.3 Theme Configuration
```css
/* Admiralty Design System custom properties */
:root {
    /* Primary Colors - Admiralty Blue */
    --ads-colour-blue: #1d70b8;
    --ads-colour-blue-darker: #144e81;
    --ads-colour-blue-lighter: #5694ca;
    
    /* Secondary Colors */
    --ads-colour-white: #ffffff;
    --ads-colour-light-grey: #f3f2f1;
    --ads-colour-mid-grey: #b1b4b6;
    --ads-colour-dark-grey: #505a5f;
    
    /* Status Colors */
    --ads-colour-green: #00703c;
    --ads-colour-red: #d4351c;
    --ads-colour-orange: #f47738;
    --ads-colour-yellow: #ffdd00;
    
    /* Typography */
    --ads-font-family: "nta", Arial, sans-serif;
    --ads-font-size-16: 1rem;
    --ads-font-size-19: 1.1875rem;
    --ads-font-size-24: 1.5rem;
    
    /* Spacing */
    --ads-spacing-1: 0.25rem;
    --ads-spacing-2: 0.5rem;
    --ads-spacing-3: 0.75rem;
    --ads-spacing-4: 1rem;
    --ads-spacing-5: 1.25rem;
}

/* Custom FSS overrides */
.fss-file-upload {
    border: 2px dashed var(--ads-colour-blue);
    border-radius: 4px;
    background-color: var(--ads-colour-light-grey);
}

.fss-file-item {
    border-left: 4px solid var(--ads-colour-blue);
    padding-left: var(--ads-spacing-3);
}
```

### 7.4 Component Examples

#### 7.4.1 ADS Button Integration
```razor
<!-- File Download Button -->
<button class="ads-button ads-button--primary" 
        @onclick="DownloadFile" 
        disabled="@IsDownloading">
    @if (IsDownloading)
    {
        <span class="ads-button__start-icon">
            <span class="ads-spinner ads-spinner--small"></span>
        </span>
    }
    Download File
</button>
```

#### 7.4.2 ADS Form Components
```razor
<!-- File Upload Form -->
<div class="ads-form-group">
    <label class="ads-label" for="file-input">
        Select files to upload
    </label>
    <input class="ads-file-upload" 
           type="file" 
           id="file-input" 
           multiple 
           @onchange="OnFilesSelected" />
</div>

<div class="ads-form-group">
    <label class="ads-label" for="description">
        File description
    </label>
    <textarea class="ads-textarea" 
              id="description" 
              rows="3" 
              @bind="FileDescription"></textarea>
</div>
```

#### 7.4.3 ADS Navigation Menu
```razor
<!-- Main Navigation -->
<nav class="ads-navigation" role="navigation" aria-label="Primary navigation">
    <ul class="ads-navigation__list">
        <li class="ads-navigation__item">
            <NavLink class="ads-navigation__link" href="/" Match="NavLinkMatch.All">
                <span class="ads-navigation__link-text">Dashboard</span>
            </NavLink>
        </li>
        <li class="ads-navigation__item">
            <NavLink class="ads-navigation__link" href="/browse">
                <span class="ads-navigation__link-text">Browse Files</span>
            </NavLink>
        </li>
        <li class="ads-navigation__item">
            <NavLink class="ads-navigation__link" href="/upload">
                <span class="ads-navigation__link-text">Upload Files</span>
            </NavLink>
        </li>
    </ul>
</nav>
```

## 8. Development Phases

### Phase 1: Foundation & ADS Integration (Weeks 1-2)
- [ ] Install and configure UKHO Admiralty Design System
- [ ] Setup authentication with Azure AD B2C
- [ ] Implement base layout using ADS page template
- [ ] Create navigation using ADS components
- [ ] Setup HTTP clients for API communication

### Phase 2: Core File Operations with ADS Styling (Weeks 3-4)
- [ ] Implement file upload component with ADS drag-drop styling
- [ ] Create file browser with ADS table components
- [ ] Add file download capabilities with ADS progress indicators
- [ ] Implement basic file operations using ADS modals and forms

### Phase 3: Enhanced Features with ADS Components (Weeks 5-6)
- [ ] Add file sharing with ADS form controls
- [ ] Implement search using ADS search component
- [ ] Create user settings with ADS form layouts
- [ ] Add file preview with ADS modal components

### Phase 4: Admin & Polish with ADS Standards (Weeks 7-8)
- [ ] Implement admin panels using ADS data tables
- [ ] Add system settings with ADS form patterns
- [ ] Apply ADS animations and transitions
- [ ] Performance optimization and GDS compliance testing

## 9. Testing Strategy

### 9.1 Unit Testing
- Component testing with bUnit framework
- Service layer testing with xUnit
- Mock HTTP clients for API testing

### 9.2 Integration Testing
- End-to-end testing with Playwright
- Authentication flow testing
- File upload/download testing

### 9.3 Performance Testing
- Load testing for file operations
- UI responsiveness testing
- Memory usage monitoring

## 10. Deployment & Infrastructure

### 10.1 Aspire Orchestration
- Leverage existing FssBlazor.AppHost for orchestration
- Configure service discovery and communication
- Setup health checks and monitoring

### 10.2 Production Considerations
- Configure Redis for session state and caching
- Setup load balancing for high availability
- Implement logging and monitoring with Application Insights
- Configure automated deployment pipelines

## 11. Configuration Requirements

### 11.1 Package Installation
```bash
# Install UKHO Admiralty Design System
npm install @ukho/admiralty-design-system

# Or via CDN in _Host.cshtml
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@ukho/admiralty-design-system/dist/css/admiralty-design-system.min.css">
<script src="https://cdn.jsdelivr.net/npm/@ukho/admiralty-design-system/dist/js/admiralty-design-system.min.js"></script>
```

### 11.2 Application Settings
```json
{
  "AzureAdB2C": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "your-tenant.onmicrosoft.com",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "CallbackPath": "/signin-oidc",
    "SignUpSignInPolicyId": "B2C_1_signup_signin"
  },
  "FileService": {
    "BaseUrl": "https://api.fileservice.com",
    "MaxFileSize": 104857600,
    "AllowedFileTypes": [".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt"],
    "UploadPath": "/uploads",
    "DownloadPath": "/downloads"
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  },
  "AdmiraltyDesignSystem": {
    "Version": "latest",
    "CustomTheme": "fss-maritime",
    "EnableAnalytics": false
  }
}
```

## 12. Success Metrics

### 12.1 Performance Targets
- Page load time < 2 seconds
- File upload progress updates < 500ms
- Search results < 1 second
- 99.9% uptime availability

### 12.2 User Experience Goals
- GDS-compliant user interface
- UKHO brand consistency
- Intuitive navigation with < 3 clicks to any feature
- Mobile-responsive design across all devices
- WCAG 2.1 AA accessibility compliance (built into ADS)
- Zero data loss during file operations

### 12.3 Design System Compliance
- 100% ADS component usage for UI elements
- Government Digital Service design pattern adherence
- UKHO brand guideline compliance
- Consistent maritime-focused user experience

This specification provides a comprehensive roadmap for implementing a modern, scalable File Share Service UI using Blazor Server with the UKHO Admiralty Design System, building upon your existing .NET 9 Aspire architecture while maintaining government digital service standards and UKHO brand compliance.