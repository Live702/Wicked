# App 

This razor library contains the UI used by both the Store.WASM and Store.Maui projects.

### SessionsViewModel 
We use a singleton ViewModel to hold the current session(s) information. This is injected into the Index.razor page and it loads the connection and tenancy configuration.

We do not use a cascading value because we are inheriting from LzCoreComponent* classes that inject, or assign view models when instantiated. 

Note that this sample app only uses a single session at a time, but the framework provides for multiple sessions in the same process. Multi-sessions would be helpful for Point of Sale terminals running a MAUI app. A single instance of the app would be launched and each session would have it's own user. The user would log in with their normal username/password and then subsequent switching among sessions could be accomplished using a short pin. This is not demonstrated in this simple single user app, but the underlying framework will support this type of implementation.

### SessionsViewModel Initialization
There is subtle difference among MAUI Hybrid apps and Blazor apps that affects when the JSRuntime is available. If you try and use JSRuntime in Main.razor, NavMenu.razor or MainLayout.razor, it will fail with a JavaScript call outside web context error. 

### Init.razor page
This page is where we do configuration for the app. It is responsible for initializing the SessionsViewModel instance that was created in MainLayout.razor. Since the MainLayout.razor inherits from LzLayoutComponentInjectViewModel<ISessionsViewModel>, it is bound to properties like IsInitialized in that view model and can conditionally show layout content based on IsInitialized.

For single session apps, we also create the first ISessionViewModel in this page.

### Loading connection configuration 
We use the JSRuntime to call JavaScript functions load the connection configuration from Blazor _content. This means you can't use the connection configuration until you reach the Init.razor page.

### Loading the tenancy configuration 
Ditto for the tenancy configuration.


## PRD - initial POC specification for the WickedApp

### Blurb Search
Blurbs. Blurbs are the only item that users interact with. 
Users may only search Blurbs. 
A Blurb is a collection of premises related by operators. 
All premises belonging to a blurb are shown. 
Example Blurb
```
The serivce interval for a 1993 Z28 is 5000 miles
< the oil must be changed every 5k miles, 3k perfered 
  >>> never tighten the drain bolt with an impact!
  > if driven hard reduce service interval further ¿(some say change oil every race)?
^ The belts should last 20k and should be checked ¿(many say they last forever, send it)?
^ Trasmission oil should be checked every 40k in auto
  > reduce to 15k if driven hard ¿(some say as low as 3k if raced)?
  >>> always change the filter 'better safe than sorry'
    < getting the filter is always a 'PITA', order it now```
```
Example premise
`never tighten the drain bolt with an impact!`

Each premise is individually clickable. 
When clicked an underline appears on the premise. no navigation occurs. A mud drawer opens an a chat appears with the words "what is your opinion on this premise" and you may comment against the premise. The system then refactors the blurb and premises to reflect your comment.

### Blurb Creation

Most of the Blurb creation process is performed by the back end service using calls to the LLM (Large Language Model). The user provides a set of ideas, and the LLM generates a blurb based on those ideas. The user can then refine the blurb by providing additional information or feedback.

During blurb creation, the LLM moves through phases
1) Identify template for candidacy, from the list of acceptable templates
2) Refactor the ideas the user provides into a list of related premises in a style similar to that of the template, as well as specifically request information it does not have and thinks it needs
3) Determine if the blurb created meets the publishing quality standards

The client app provides a UI for the user to enter their ideas, and the LLM processes these ideas to create a blurb. The user can then review the blurb and provide feedback or additional information to refine it further. This is primarily done in a chat-like interface where the user can interact with the LLM and provide input on the blurb.

### Blurb Refactor
The blurb refactor process is a collaborative effort between the user and the LLM. The user can provide feedback on the blurb, and the LLM will use this feedback to improve the blurb's clarity and utility. The LLM will also use its own knowledge and understanding of the topic to refine the blurb further.

This primarily happens in the back-end service , where the LLM processes the user's feedback and refines the blurb accordingly. The user can then review the updated blurb and provide additional feedback or information as needed.

### User Interaction
The user interacts with the blurb through a chat-like interface, where they can provide feedback and ask questions about the blurb. The LLM processes this feedback and updates the blurb accordingly.

#### Search
The user can search for Blurbs and view the premises of the Blurb. The user can click on a premise to open a chat drawer where they can provide feedback. This interaction allows the user to refine the blurb and its premises further. The back-end may crerate a new Premise based the user's feedback, which will be added to the blurb. The user doesn't directly control the creation of new premises; instead, they provide feedback that the LLM uses to generate or refine premises.

#### New Blurb Creation
The user can also create a new Blurb. This takes the user to a chat interface where they can provide their ideas and feedback. The LLM will then process this information to create a new Blurb and one or more Premises based on the user's input. 

There are two forms of blurb feedback provided by the user:
#### Chat

#### Voting
User's can vote on the premises of a blurb. This voting is used to determine the accuracy of each premise.

### UI/UX 
Login page 

## MainPage 
### Left Drawer Menu
    Blurbs
    Your Input
    Profile

#### Blub Content 
    Search Blurbs
    Add Blurb - selecting opens the BlurbDetailPage for a new blurb creation
    Blurb List - selecting an item opens the BlurbDetailPage

#### Your Input 
    Search Blurbs 
    BlurbList - selecting an item opens the BlurbDetailPage

#### Profile 
    User Profile - shows the user's profile information and allows them to update their profile

#### BlurbDetailPage
    Blurb Summary 
    Blurb Premises - shows the premises of the blurb and allows the user to provide feedback on each premise
        Voting
        Badges - show the blurbs user has earned for their contributions to the blurb premise
    Chat Drawer - allows the user to provide feedback on the blurb and its premises
   
   NB: When we are creating a new Blurb, the Blurb Summary and Blurb Premises sections are not shown. We open the chat drawer directly to allow the user to provide their ideas and feedback.

   NB: The ChatDrawer may be a separate page instead of a componet in the BlurbDetailPage. This works because the LLM responses tell the user what changes it will or wants to make to the blurb and premises. The user can then provide feedback on those changes in the chat drawer directly and see these by returning to the BlurbDetailPage. Reopening the ChatDrawer will allow them to continue the converation on the current premise or they may select a different premise to chat about that premise.


#### ChatDrawer 
   Chat messages - shows the chat messages between the user and the LLM
   When the user returns to a chat after leaving it, the chat drawer will display a summary of the previous interactions rather than a full chat history.
   Input box - allows the user to provide feedback or ask questions about the blurb or its premises


#### Chat Interaction
   The user can interact with the LLM through the chat interface, providing feedback and asking questions about the blurb and its premises.
   The LLM will process this feedback and update the blurb and its premises accordingly.
   The user can also provide additional information or feedback to refine the blurb further.
   The LLM will suggest the creation/update of Premises based on the user's feedback, which the user can then review and approve or reject.


## Musings

### Blurb Creation 

Option1: We create a Blurb, Premise, and Chat all at once. We use state in Blurb and Premise to track the creation process.

Option2: We create a Chat without Blurb or Premise. The Chat is used to gather information from the user. The backend creates a Blurb and Premise based on the Chat messages. The user can then review the Blurb and Premise and provide feedback.

Question: What if there are more than one Premise associated with the Chat?

Do we just associate the Chat with the Blurb?

What does it mean to selecte a Premise to Chat about? - does this just help establish context?

Should there be only one user Chat per Blurb? We just keep a list of "focus Premises" that the user can select from?". We can have an "All" option. The list of focus Premises can change during the chat, allowing the user to select different focus premises to focus on.

Once you are chatting about a Blurb, how to easily reference existing Premises belonging to the Blurb?

Can we select one or more Premises in a Blurb to expand the context/focus of the Chat? This doesn't mean other premises are ignored, just that we are focusing on the selected premises; perhaps to establish a logical relationship between them?

Does the creation of the first Premise indicate that a Blurb is publicly viewable or does the user have to explicitly publish the Blurb?

What happens if multiple users are creating Blurbs that end up being similar? Do we merge them?

Design:
- BlurbChatPage - a page that allows the user to create/edit Blurbs and its Premises through a chat interface
    - BlurbDetailsView - a component that shows the details of a Blurb, including its Premises (hidden until Blurb created)
    - ChatMessagesView - a component that shows the previous chat messages between the user and the LLM
    - PremiseFocus - a component that allows the user to select a Premise to focus on in the chat (hidden until Blurb created)
    - ChatInput - a component that allows the user to provide feedback or ask questions about the blurb and its premises
        - Backend creates a Blurb and Premise based on the Chat messages
- Parameters
    - ChatViewModel - the view model for the chat interface
    - We create Chat instances with or without Blurbs - this depends on the navigation
        - Create with Blurb context when we navigate to the page from a selected Blurb in the BlurbsList 
        - Create without Blurb context when we navigate to the page from the Add Blurb button in the BlurbsList

- ChatsPage - a page that shows the list of Chats for the user
    - ChatListView - a component that shows the list of Chats for the user
    - User clicks on Chat to open the BlurbChatPage with the Chat context

