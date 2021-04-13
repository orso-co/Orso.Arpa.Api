# Orso.Arpa Tests

## Abstract
The tests of the app.

## execution order of tests

The OrderAttribute may be placed on a test method or fixture to specify the order in
which tests are run within the fixture or other suite in which they are contained.
Ordering is given by the required order argument to the attribute, an int.

The OrderAttribute follow this numbering schema:

index       | type of tests
-----       | ------
1 - 99      | test that get values from the data base
100 - 999   | test that modify value in the data base
1000 - 9999 | test that create new entries in the data base
10000+      | tests that delete values from the data base

