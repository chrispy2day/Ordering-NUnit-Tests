Ordering NUnit Tests
====================
This repository shows how you can create ordered tests in NUnit, which you might want when running integration, acceptance, or other end-to-end tests.  This repository accompanies [my blog post](http://www.jhaichrispy.com/blog/2014/11/ordering-nunit-tests) on this topic, so for a fuller description on the topic please go there.

The examples in the repository are based on a very cool approach from [this StackOverflow question](http://stackoverflow.com/questions/1078658/nunit-test-run-order).  There are 2 great code samples provided that I have copied into the project to make it easier to view them side by side.  Example 1 is the original code sample that uses NUnit's `TestCaseSource` in a very clever way to generate the ordered tests.  Look at the code for the specifics, but basically he returns each of the tests in an `IEnumerable`, where you would normally have your data.  A lambda expression provides the implementation.  Super smart approach and kudos to the author.

The next example extends this approach to be a little more developer friendly by utilizing an attribute.  This way you can write your test methods as usual and just decorate them with an attribute that controls the order they will run in.  Much nicer syntax and I loved the idea.  Take a look at the code for Example 2 to see how this was implemented.  No really, go ahead, I'll wait...

Did you read the code?  Did you notice how the author uses reflection and his attribute to pull out the test methods.  Pretty smart right?!  Yeah, I like this approach and it makes for a very clean class.  However, there's a bit of a problem, or at least I viewed it as a problem.  His reflection is pulling methods for every class in the assembly, not just the current class.  This could end up with you running more tests than you actually thought you were going to.  I added in the `Example2b` class to show this.  If you run the tests for Example 2, you will get all the tests run for both the original class as well as my additional class.  Maybe not what you intended.

Finally, for Example 3, I put my own spin on the approach.  I fixed up the reflection code from 2 and pushed all the setup to make this work to another file to show how you might actually want to use this in production.  I also removed the `Int` class from the code because I felt it was ugly and unnecessary.  You'll notice now that you can run tests from 3A or 3B and only the corresponding tests will run.  I think this makes for some lovely code.  

Have a look at the code and have fun testing!  Hope that helps my fellow devs.  Happy coding!

Chris Plowman
[JhaiChrispy](http://www.jhaichrispy.com)