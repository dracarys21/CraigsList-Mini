# Requirements

Requirements that the application needs to conform to

## General

1. Site must have a name
2. Every page **MUST** have a title
3. Site **MUST** be validated using an HTML validator
4. All Pages must meet the following criteria:
    - Easy to understand and use without direction
    - Page should contain a reasonable amount of content
    - Page elements are reasonably spaced
    - All input is validated and encoded
    - No unintended errors should occur (Broken links, exceptions, and other anomalies)
5. **No** post should appear on the homepage

## Authorization

How users will interact with the system

1. Anonymous
    - Read public posts
2. User
    - Create, modify, and delete their own posts
    - Read and comments on other public posts
3. Admin
    - All of the user functionality
    - Set up and configure website

4. First user to register in the system must be placed in the Admin role. Any user after that must get their `Admin` role status by having a user in the Admin role promote their account.

## Site Behavior

How the site should be laid out and behave in general based on the requirements in the project specifications

### Homepage

Requirements that must be met on the homepage.

#### Category and SubCategory

1. All SubCategories must be grouped with its respective Category
2. All Categories and SubCategories must hyperlink to the `Categories` screen

#### Location

1. Users should be able to select a [Location](docs/Models.md) that corresponds to where the user wants to search for posts.
2. The site must prominently identify the currently selected location near the top of the page
3. Location should be sorted in Alphabetical order
4. The user should be able to select either an entire area or one specific locale within a specified area
5. Selecting a location will not directly affect what is shown on the home page. However, if the user clicks any Category or SubCategory on the home page (to go to the categories screen), the posts shown on these screens should be restricted to only those posts within the location selected on the homepage
6. Location can be defaulted to any arbitrary area and/or locale

#### User Specific Behavior

What is to be displayed to a user in the Admin or User role

1. Create Posts
    - There must be a `Create Post` link or button
    - The `Create Post` page must take information from the homepage to prepopulate input fields in the form.
    - If the users gets to the `Create Post` screen from anywhere but the homepage all information must be manually entered
    - If a user is **NOT** logged in, there should be text instructing the user to log in to create the post

### List Posts (Requires Auth)

Page containing all post **OWNED** by the currently logged in user

1. Users can see a list of **their** own unexpired and expired posts
2. User should be able to Create, Modify, or Delete active posts
3. Expired Posts can only be viewed, but **NOT** modified or Deleted.
4. Posts should not be shown as expired if they were deleted before they expired
5. This page must be paginated
6. A user must be given a confirmation page before deleting their post

### Create Post (Requires Auth)

Page that will be displayed when a user clicks on `Create Post`

1. Can only see the contents of this page if the logged in user is in the Admin or User role
2. If unauthenticated, the user will see text instructing them to log in
3. All fields displayed to the user are required

### Modify Post (Requires Auth)

Page a user will be taken to when they want to edit a post

1. Should look like the `Create Post` page
2. This page should **NOT** be available if the post was deleted or expired
3. The `LastModifiedDate` and `LastUpdatedBy` fields in the post should be updated if a modification is made
4. Owner, expiration, and unique identifier should be displayed but **CANNOT** be modified
5. Owner, expiration, and unique identifier **MUST** be explicitly ignored in the POST method. Use [Bind] in the controller

### Categories

Shows posts for the selected `PostType` and `Location`

1. If a category is specified without a subcategory, then ALL posts in the category (spanning all subcategories) should be displayed
2. If both Category and SubCategory are selected it should only display the posts that met that criteria
3. If a Location is specified without a local, then ALL posts in that Area (spanning all locales) should be displayed
4. If both Area and Locale are specified then posts should be filtered with that criteria
5. All these search criteria can be mixed and matched
6. Screen is viewable by all Users
7. The screen **MUST** be paginated
8. The page must receive information concerning `PostType` and `Location` via query
9. User should be given an option to modify these vales on the page
10. Page will contain a List View of posts that are text-based with hyperlinks to the post detail page.
11. If user is authenticated, they should be able to see a `Response Screen` where they can enter a response

### Response Screen

Screen displayed to a user so they can response to a post

1. Freeform textarea control for body of message

### Inbox Screen (Require Auth)

Screen that allows users to see their communications to posts

1. These messages are private between the sender and receiver
2. Communication between users can continue despite the state of the Post (Deleted or expired)

### Admin Screens (Requires Auth)

Screen is only visible to users in the Admin role

1. Ability to change the role of any user in the `User` role to the `Admin` role. This is one way.
2. CRUD for Categories and SubCategories
3. CRUD for Areas and Locals
4. List or Delete posts for any user in the system
