# Business Logic

What logic is used throughout the application

## Post

1. Create a post
2. Edit a post
3. Delete a post
4. Get details of a specific post
5. Respond to a post
6. List all posts by owner
7. Filter posts by a combination of (PostType and/or Location). Include logic for pagination and sorting
8. Respond to a Post

## Inbox

1. Respond to a message (This is different than responding to a post). Second leg of communication. When responding to a message you must create a message object and add the new message to the collection of messages for that post
2. List messages by post
3. Delete a response
4. Mark a message read (This could be done on page load or button) - Might not require a method in the business logic layer

## Location

> Don't think any of these methods require "Business logic". We won't be doing any strong validatation

1. Create locations (Admin only)
2. Edit locations (Admin only)
3. Delete locations (Admin only)
4. Update locations (Admin only) - (i.e. Active status or slug)

## Post Types

> Don't think any of these methods require "Business logic". We won't be doing any strong validatation

1. Create PostType (Admin only)
2. Edit PostType (Admin only)
3. Delete PostType (Admin only)
4. Update PostType (Admin only) - (i.e. Active status or slug)

## User Management

1. Update user role
2. Make the first user to register an admin
