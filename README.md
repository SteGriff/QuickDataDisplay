# QuickDataDisplay

This is an ASP.Net (webforms) application which executes SQL queries from the `/sql` directory and draws the results in an HTML table. Because QDD can read any-shaped data, we don't use strongly typed models.

## Setup

 * Set your connection string as `DataConnection` in `web.config`
 * Make an SQL file and put it in the `/sql` directory
 * Start the site in VisualStudio 2010+, or host it in IIS
 
## Features

 * Page refreshes every 10 seconds
 * Shows the results of all query files in `/sql` directory
 * You can modify the query files on the fly
 
## Maintenance Questions

### How does the data model work?

See `Model/DataTable.cs` - The data is stored as a `List<object[]>` alongside column headers stored as `List<string>`

### Where is the View which renders the table?

It is `Default.aspx`

### How do I change the look & feel?

QDD uses [Tachyons](https://tachyons.io) for a quick-and-easy table format. To alter the visuals, change the stylesheet link in `Site.master` and the CSS classes used in `Default.aspx` 

### How do I change the refresh time?

Change the `<meta http-equiv="refresh" content="10">` line in `Site.master` to some other value (in seconds)

### What if my SQL has side-effects?

Each SQL file will be executed as command text with `command.ExecuteReader()` upon *every refresh interval*, so any side-effects will be re-run each time the page refreshes. This may or may not be desirable, but if you just want to read data, make sure the SQL in your query file does not have any side-effects.
