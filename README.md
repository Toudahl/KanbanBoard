# KanbanBoard
A simple kanban board made as a "brush up" exercise at the beginning
of my second semester studying for my AP graduate degree in computer science.

The board uses Gong drag and drop, and newtonsoft json for persistence.

Done refactoring - onwards to more features.

// TODO: Create a way to dynamically add more boards.

// TODO: Create functionallity to add employees

// TODO: Add other types of persistence.

// TODO: Make a check on the file beeing loaded to detect what type of persistence was used, and
// automatically select the correct class to deserialize.

// TODO: Further clean up of the UI

// TODO: Create settings functionallity

     Auto load
     
     Default persistence type
     
     Auto save
     
// TODO: refactor again at a later point

Known bugs:
None at the moment.

Fixed bugs:
Casting objects deserialized from newtonsoft json only works when using the build in
JsonConvert.DeserializeObject<T>. If i pass on the object through my PersistenceHandler, and try to cast it
where it is beeing used. The casting will fail.

Credit goes to: http://stackoverflow.com/users/2345956/ezi


Feel free to use my project. But give credit for it.

If you end up earning money off it - good for you! :)
