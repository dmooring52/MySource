function EditQueueEntry(eventkey, singerkey, round)
{
    var hi = eventkey;
}

function WelcomeWorld()
{
    return "Hello";
}

function SetState(s) {
    document.getElementById("finished").className = "img";
    document.getElementById("gonehome").className = "img";
    document.getElementById("nothere").className = "img";
    document.getElementById("pending").className = "img";

    if (s == 'f') {
        document.getElementById("theQueueState").value = "Finished";
        document.getElementById("finished").className = "img-black";
    }
    else if (s == 'g') {
        document.getElementById("theQueueState").value = "Gone Home";
        document.getElementById("gonehome").className = "img-black";
    }
    else if (s == 'n') {
        document.getElementById("theQueueState").value = "Not Here";
        document.getElementById("nothere").className = "img-black";
    }
    else if (s == 'p') {
        document.getElementById("theQueueState").value = "Pending";
        document.getElementById("pending").className = "img-black";
    }
}
/*
function CutCopyPaste(s)
{
    if (s == 'paste')
        pasteToClipboard();
    else if (s == 'copy')
        copyToClipboard();
    else if (s == 'cut')
        cutToClipboard();
}
function pasteToClipboard()
{
    var clipText = window.clipboardData.getData('Text');
    var clip = clipText.split("\t");
    if (clip.length > 2)
    {
        if (clip.length == 4)
        {
            document.getElementById("QueueSong").value = clip[0];
            document.getElementById("QueueArtist").value = clip[1];
            document.getElementById("QueueNote").value = clip[2];
            document.getElementById("QueueLink").value = clip[3];
        }
        else if (IsLink(clip[clip.length - 1]))
        {
            document.getElementById("QueueSong").value = clip[0];
            document.getElementById("QueueArtist").value = clip[1];
            document.getElementById("QueueLink").value = clip[clip.length - 1];
        }
    }
    else
    {
        if (IsLink(clip[0]))
        {
            document.getElementById("QueueLink").value = clip[0];
        }
    }
}

function cutToClipboard()
{
    copyToClipboard();
    document.getElementById("QueueSong").value = "";
    document.getElementById("QueueArtist").value = "";
    document.getElementById("QueueNote").value = "";
    document.getElementById("QueueLink").value = "";
}

function copyToClipboard()
{
    var song = "";
    var artist = "";
    var note = "";
    var link = "";
    if (document.getElementById("QueueSong").value != null)
        song = document.getElementById("QueueSong").value.toString().trim();
    if (document.getElementById("QueueArtist").value != null)
        artist = document.getElementById("QueueArtist").value.toString().trim();
    if (document.getElementById("QueueNote").value != null)
        note = document.getElementById("QueueNote").value.toString().trim();
    if (document.getElementById("QueueLink").value != null)
        link = document.getElementById("QueueLink").value.toString().trim();

    if (song.length > 0 || artist.length > 0 || note.length > 0 || link.length > 0)
    {
        window.clipboardData.clearData('Text');
        var cells = "{0}	{1}	{2}	{3}".format(song, artist, note, link);
        window.clipboardData.setData('Text', cells);
    }
}
*/

function cutboard()
{
    copyboard();
    document.getElementById("QueueSong").value = "";
    document.getElementById("QueueArtist").value = "";
    document.getElementById("QueueNote").value = "";
    document.getElementById("QueueLink").value = "";
}
function copyboard()
{
    if (document.getElementById("clipbox") != null)
        document.getElementById("clipbox").value = "";
    var song = "";
    var artist = "";
    var note = "";
    var link = "";
    if (document.getElementById("QueueSong").value != null)
        song = document.getElementById("QueueSong").value.toString().trim();
    if (document.getElementById("QueueArtist").value != null)
        artist = document.getElementById("QueueArtist").value.toString().trim();
    if (document.getElementById("QueueNote").value != null)
        note = document.getElementById("QueueNote").value.toString().trim();
    if (document.getElementById("QueueLink").value != null)
        link = document.getElementById("QueueLink").value.toString().trim();

    if (song.length > 0 || artist.length > 0 || note.length > 0 || link.length > 0) {
        var cells = "{0}	{1}	{2}	{3}".format(song, artist, note, link);
        if (document.getElementById("clipbox") != null)
            document.getElementById("clipbox").value = cells;
        if (document.getElementById("clipbox") != null)
            document.getElementById("clipbox").select();
        ///window.clipboardData.clearData('Text');
        ///window.clipboardData.setData('Text', cells);
    }
}
function pasteboard()
{
    var clipText = document.getElementById("clipbox").value;
    if (clipText != null) {
        var clip = clipText.split("\t");
        if (clip.length > 2) {
            if (clip.length == 4) {
                document.getElementById("QueueSong").value = clip[0];
                document.getElementById("QueueArtist").value = clip[1];
                document.getElementById("QueueNote").value = clip[2];
                document.getElementById("QueueLink").value = clip[3];
            }
            else if (IsLink(clip[clip.length - 1])) {
                document.getElementById("QueueSong").value = clip[0];
                document.getElementById("QueueArtist").value = clip[1];
                document.getElementById("QueueLink").value = clip[clip.length - 1];
            }
        }
        else {
            if (IsLink(clip[0])) {
                document.getElementById("QueueLink").value = clip[0];
            }
        }
    }
}
function IsLink(link)
{
    return (link.indexOf('/') > 0 || link.indexOf('\\') > 0);
}

if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}