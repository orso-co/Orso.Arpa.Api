CREATE TABLE "LocalizationRecords" (
    "Id" BIGINT NOT NULL CONSTRAINT PK_LocalizationRecords PRIMARY KEY IDENTITY,
    "Key" NVARCHAR(MAX),
    "ResourceKey" NVARCHAR(MAX),
    "Text" NVARCHAR(MAX),
    "LocalizationCulture" NVARCHAR(MAX),
    "UpdatedTimestamp" DATETIME NOT NULL
)

CREATE TABLE "ExportHistoryDbSet" (
    "Id" BIGINT NOT NULL CONSTRAINT "PK_ExportHistoryDbSet" PRIMARY KEY IDENTITY,
    "Exported" NVARCHAR(MAX) NOT NULL,
    "Reason" NVARCHAR(MAX)
)

CREATE TABLE "ImportHistoryDbSet" (
    "Id" BIGINT NOT NULL CONSTRAINT "PK_ImportHistoryDbSet" PRIMARY KEY IDENTITY,
    "Imported" NVARCHAR(MAX) NOT NULL,
    "Information" NVARCHAR(MAX)
)
