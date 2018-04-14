USE MusicInside
GRANT EXECUTE ON OBJECT::dbo.sp_GetAllAlbum TO [IIS APPPOOL\NoManagedPool]
GRANT EXECUTE ON OBJECT::dbo.sp_GetAllArtist TO [IIS APPPOOL\NoManagedPool]
GRANT EXECUTE ON OBJECT::dbo.sp_GetAllSong TO [IIS APPPOOL\NoManagedPool]
GRANT EXECUTE ON OBJECT::dbo.sp_GetAllSongStartsWith TO [IIS APPPOOL\NoManagedPool]
GO