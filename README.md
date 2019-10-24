# SCA-Knight-Squire-Project

Tracking Knight Squire Relationships and Lineages for the SCA

## Goal

I hope to track the knight-squire relationships throughout the SCA known world.

### 24 OCT 2019 - 1:31 PM UPDATE AND UPLOAD

Got my program to actually function the way i want so there aren't multiple entries for people. Woot!! Now to match up the names in the ks_relationship file to the names in the WhiteBelt spreadsheet. Whoever decided to put names in that aren't ascii letters ... i really dislike you.

### 21 OCT 2019 - 8:06 AM UPDATE AND UPLOAD

More changes from over the weekend. Added contribution instructions and the link to the google form in the readme. Also added new powershell scripts for creating the data needed to make the html file.  Also working on a script to tell me the differences between the people in the ChivList file compared to the file of relationships ... basically a query to ell me who i am missing.

### 17 OCT 2019 - 4:35 AM UPDATE AND UPLOAD

Jenn not feeling well so i woke up and worked on this some more. Wrote a C# program to create the data file which combines the whitebelt spreadsheet with my relationships XML data. BTW: [Linq to XML](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/linq-to-xml-overview) is badass.I was able to update the doc in memory and spit out the result in xml ... only takes about 30 secs to run.  For some reason the changes i made to the way im storing the xml is causing people ot show up twice ??? Ill look at it a little later today see if i can get the correct results.

-- Clint aka Cathal

### How this all started (Once upon a time ...)

A few years ago Carl Chipman (Duke Jean Paul de Sens) created a document where he was tracking the knight - squire relationships of the people he knew. you can see it [here](http://chivalry.ansteorra.org/new/knm.pl). I thought it was pretty cool and intersting to see who trained whom. It also used to tell you alot about what type of fight you would get from the person. Trainers and students all have tendencies.

A year or 2 ago i was on the Known World Chivalry Facebook group and found out about this little [gem](http://www.whitebelt.com/CHIVPROJECT/). This is the Order of Precedence for the Order of Chivalry for the SCA. This data is maintained by Viscount Edward Zifran of Gendy, KSCA, OL, OP, ETC, secrectly referred to as 114-K103-60.

So i sat there for a while and thought ... h'm. Wouldn't it be cool to track the knights and squires along with all the data that is found in the OP. Things like date elevated, kingdom elevated, what is my overall society chiv number.  That would be cool to see.

I also wanted to give everyone the ability to contribute. Give everyone the ability to update the data and say "Hey ... soandso is my squire". Instead of creating a bunch of code, webpages, databases and crap ... why dont we just track with updates using GitHub. Then anyone can add a pull request to update the data. Or at the very least post a bug saying hey ... the data on here is wrong please update it. Thats what GitHub is great at.

## How to contribute

- Email me: [cgallon@gmail.com](mailto:cgallon@gmail.com)

- Fill out this lovely google [form](https://forms.gle/ZV4DASdx5zwZREVk8) made by Dan McEwan.

- Create an issue in GitHub
  - Create an account in GitHub
  - Click this link for a new [issue](https://github.com/ClintGallon/SCA-Knight-Squire-Project/issues/new)

- Write some code
  - Read this [blog post](https://product.hubspot.com/blog/git-and-github-tutorial-for-beginners)
  - Install [Git](https://git-scm.com/downloads)
  - Create a GitHub account
  - Clone the repo
  - Write some code
  - Push the changes to the repo
  - Add a pull request
  - Bask in your Git/GitHub glory

Anyone who wants to contribute add a pull request or post a bug.

Thank you,

Clint Gallon

Sir Cathal Finn O'Briain

Atenveldt
