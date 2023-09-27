redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51Mc6JmSJkIBjnVv9A9KFmhYdQ03Cp9jYH0RUxfuZqQG2wQqG81WwP6ULN5EvP3YwU0TvMGhoyLqxVKtfzr7QhzCJ00DE2AqGQb');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};