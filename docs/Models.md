# Models

A high level overview of the Data Model

## Posts

| Field | Data Type | Nullable? | Default Values | Notes
| ---|---|---|---|---|
| Id | int | No | system generated | PK |
| CreateDate | DateTime | No | system time | |
| LastModifiedDate | DateTime | No | system time | |
| ExpirationDate | DateTime | Yes | system time | this should be nullable until the user "Publishes" their Post. In other words, until they "Publish" the post is private and only viewable by the owner |
| CreatedBy | ApplicationUser | No | | Identity User Object |
| LastUpdatedBy | ApplicationUser | No | | Identity User Object |
| Title | string | No | | we should probably set a limit, maybe (140 or 255) Characters? |
| Body | string | No | | While I don't think we require a character limit on this field we should keep in mind that users may include basic html tags? Although that is not something outlined in the assignment we may just want to keep that in mind |
| Location | LocationModel | No | | FK |
| PostType | PostTypeModel | No | | FK |
| Deleted | bool | No | false | |

## Inbox

Keeps track of converstations per post

| Field | Data Type | Nullable? | Default Values | Notes
| ---|---|---|---|---|
| Id | int | No | system generated | PK |
| Post | Post | No | | FK |
| Messages | List<Message> | No | | The first Post response will create a record and any subsequent responses can be added to this list |

## Message

| Field | Data Type | Nullable? | Default Values | Notes
| ---|---|---|---|---|
| Id | int | No | system generated | PK |
| Message | string | No | | this **MUST** be encoded to security |
| SendTo | ApplicationUser | No | | Identity Users Object |
| CreateDate | DateTime | No | system time | |
| CreatedBy | ApplicationUser | No | | Identity Users Object |
| Deleted | bool | No | false | |
| Read | bool | No | 0 | |

## Locations

| Field | Data Type | Nullable? | Default Values | Notes
| ---|---|---|---|---|
| Id | int | No | system generated | PK |
| Area | string | No | | Values would be States (i.e. NY, FL, etc.)
| Locale | string | No | | Values would be City or County (i.e. NYC, Brooklyn, etc.)
| Slug | string | No | | this should be limited in length and values. I'm thinking '^[a-zA-Z0-9-]+$'. <br>So be clear, any alphanumeric characters including hyphen. We may want to extend validation to ensure that both the start and end of the slug is a letter and maybe limit the length to 32 characters.
| Active | bool | No | false | Indicates whether or not we should display this location

## PostTypes

| Field | Data Type | Nullable? | Default Values | Notes
| ---|---|---clear, any alphanumeric characters including hyphen. We may want to extend validation to ensure that both the start and end of the slug is a letter and maybe limit the length to 32 characters.
| Active | bool | No | false | Indicates whether or not we should display this Post Type

## Comments/Notes/Questions

1. Should `slug` be its own object?
    - Pros:
        - The benefit here would be that validation would be contained to one object.
        - It would be easier to unit test to ensure validation is correct.
        - If we need to update its behavior it would all be contained in one place (i.e. change max size, allow other characters in slug, etc.)
    - Cons:
        - It only appears in 2 tables
        - Once set up it won't change

2. We should agree on sensible defaults. 
    - Length limits on fields (i.e. title, body, etc.)
    - What fields are nullable
    - Any special treatment to certain fields
    - etc.
