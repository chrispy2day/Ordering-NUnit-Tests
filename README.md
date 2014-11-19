Ordering-NUnit-Tests
====================

This repository shows how you can create ordered tests in NUnit.  Now before you start screaming that unit tests should not have any order dependencies, let me assure you that I know and agree wholeheartedly.  So why create this project and bother with showing how to do this?  Simple: there are other kinds of tests besides unit tests, and they often DO have order dependencies.

For example, if you are running database integration tests, a super basic CRUD test might look like this:

1. Perform initial get and assure empty.
2. Add an item.
3. Get new item back.
4. Delete item
5. Get empty list again.

Since we want to run this integration all the way through on a real database (hopefully NOT production!), this should be run in order.  Without ensuring the order, we would have to do complex, wasteful, and time consuming setup for each step of our tests.

Of course, we have a couple options for doing this ordered test, including not using a testing framework at all.  However, NUnit is a great testing framework that many developers are already very familiar with and are likely using for their actual unit tests, so why not leverage this knowledge to make our lives a little easier?  Alas, NUnit has been built for unit tests, so it doesn't have any built in mechanisms for running tests in order, but with a little ingenuity it can absolutely be accomplished.

A simple way to run an ordered test would just be to build up one test method with multiple steps and assertions along the way.  I have done this in the past for some simple tests like the one above.  The nice thing with this approach is that if an assertion fails early on, the test will fail without continuing execution.  Often this is exactly what you want with ordered tests since if your preconditions are not met, your later tests may not be valid.  However, as your tests become more complicated, this single test becomes long and difficult to work with.  Also, if you do want to continue after an initial assertion fails, you can't.  So this is not a great option for real world development on big projects.

Another option is to prepend an alpha character to your tests.  NUnit orders tests alphabetically, so this way you can control the running order.  It's obviously a bit of a hack, can be difficult to maintain, and makes for some ugly method names.  The other problem is that it could break at any time if NUnit decides to order their tests differently.

I recently stumbled across a very cool approach on [this StackOverflow question](http://stackoverflow.com/questions/1078658/nunit-test-run-order).  There are 2 great code samples provided that I have copied into the project to make it easier to view them side by side.  Example 1 is the original code sample that uses NUnit's `TestCaseSource` in a very clever way to generate the ordered tests.  Look at the code for the specifics, but basically he returns each of the tests in an `IEnumerable`, where you would normally have your data.  A lambda expression provides the implementation.  Super clever approach and kudos to the author.

The next example extends this approach to be a little more developer friendly by utilizing an attribute.  This way you can write your test methods as usual and just decorate them with an attribute that controls the order they will run in.  Much nicer syntax and I loved the idea.  Take a look at the code for Example 2 to see how this was implemented.  No really, go ahead, I'll wait...

Did you read the code?  Did you notice how the author uses reflection and his attribute to pull out the test methods.  Pretty smart right?!  Yeah, I like this approach and it makes for a very clean class.  However, there's a bit of a problem, or at least I viewed it as a problem.  His reflection is pulling methods for every class in the assembly, not just the current class.  This could end up with you running more tests than you actually thought you were going to.  I added in the `Example2b` class to show this.  If you run the tests for Example 2, you will get all the tests run for both the original class as well as my additional class.  Maybe not what you intended.

Finally, for Example 3, I put my own spin on the approach.  I fixed up the reflection code from 2 and pushed all the setup to make this work to another file to show how you might actually want to use this in production.  I also removed the `Int` class from the code because I felt it was ugly and unnecessary.  You'll notice now that you can run tests from 3A or 3B and only the corresponding tests will run.  I think this makes for some lovely code.  Have a look at the code and have fun testing!

Hope that helps my fellow devs.  Happy coding!

Chris Plowman
[JhaiChrispy](http://www.jhaichrispy.com)