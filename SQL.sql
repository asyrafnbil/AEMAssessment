SELECT PW.PlatformName,
PW.id as Id,
PW.platformId as PlatformId,
PW.uniqueName as UniqueName,
PW.latitude as Latitude,
PW.longitude as Longitude,
PW.createdAt as CreatedAt,
PW.updatedAt as UpdatedAt
FROM (
SELECT p.uniqueName as PlatformName ,W.*, ROW_NUMBER() OVER(PARTITION BY W.platformId ORDER BY W.updatedAt DESC) AS RN
FROM [dbo].[Well] W
JOIN [dbo].[Platform] P on W.platformId = P.id
) as PW
where RN = 1