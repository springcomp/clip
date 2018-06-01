# clip

A simple command-line replacement for the MS-DOS clip.exe tool.

## Rationale

I like to be able to clip one-liner into the Windows clipboard from a PowerShell prompt with a command like so:

```PS
PS> $PWD.Path | clip
PS> [Guid]::NewGuid().guid | clip

```

However, when used this way, the CLIP command-line tool adds an extra line to the clipboard.