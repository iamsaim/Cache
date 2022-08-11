# Implemented small onion architecture.
# Used .NET 6.
# Set Cache size to 5, on adding a new item oldest accessed item will be removed.
# When an item is removed, information is logged. Email triggers can be used there to notify user.
# Service works on singelton pattern.
# Exposed different endpoints which makes this solution flexible enough to manage cache.
# Added swagger for understanding and testing of APIs.