# CheersUnityExtensions
A collection of handly utility functions - collected over ten years using Unity.

Here's a quick summary of some of the more notable functions in the package:

## [OnEnterPlay] attributes

For use with the "no domain reload on play" project setting - apply this family of attributes (e.g. [OnEnterPlay_Set(0)] or [OnEnterPlay_SetNull] or [OnEnterPlay_Clear] to static variables to reset them when entering play, or add [OnEnterPlay_Run] to a static function that you want to run on entering play.

## Ray.ProjectToLine, ProjectToPlane

Ray functions that project the Ray to an arbitrary plane, or snap to the nearest point on a line.

## Quaternion.ToLocal, ToWorld

Quaternion extension methods that convert to/from a parent rotation.

## Transform.TransformRotation, InverseTransformRotation
 
Transform extension methods, continuing the pattern of TransformPoint, InverseTransformDirection, etc.

## List.FindMin/FindMax, ItemWithMin/ItemWithMax, IndexOfMin/IndexOfMax, ListItemsWithMin/ListItemsWithMax:
A family of List/Array extension methods that either -
   - Return the smallest/largest item in the list.
   - Apply a function to each list item, then return the smallest/largest result.
   - As above, but return the *item that produced* the smallest/largest result.
   - As above, but return the *index of* that item within the list.
   - As above, but return *the list of* all items that got the equal smallest/largest result.
   
## List.PopItemAt(index), PopAndSwapItemAt(index), PopRange(index, count)
   - A list function: Remove the item at the given index, and return the item.
   - As above, but swap the last element of the list into the freed slot. (More efficient than shifting the whole list down.)
   - As above, but remove a whole range of items from the list. Return them as a new list.

## Dictionary.GetOrFallback, GetOrAdd
A family of Dictionary extension methods that find a dictionary element by its key, and do something if it's missing.
   - In the case of GetOrFallback, they just return another value - for example, get the default(T) for that type, or create a new T(), or run a function, or look up that key in a different dictionary.
   - In the case of GetOrAdd, the same, but the value found is added to the dictionary.

## Dictionary.Mutate, Concat, AddItem, AddAmount
A family of Dictionary extension methods that get an element by its key, apply a function to it, then write the result back into the dictionary. If the specified element is missing, there's a range of options for initializing it, similar to GetOrFallback.

Also offers a variety of specialized versions:
   - **dict.Concat(key, string)** - for dictionaries of strings: concat this string to the end of the string at dict[key]. Fallback = empty string.
   - **dict.AddItem(key, item)** - for dictionaries of lists: append this item to the end of the list at dict[key]. Fallback = create an empty list.
   - **dict.AddAmount(key, amount)** - for dictionaries of numbers: add this value to the number at dict[key]. Fallback = 0.
