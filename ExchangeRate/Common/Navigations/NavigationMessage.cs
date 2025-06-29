namespace ExchangeRate.Common.Navigations
{
    class NavigationMessage
    {
        public Type TargetPageType { get; }
        public object? Parameter { get; }

        public NavigationMessage(Type targetPageType, object? parameter = null)
        {
            TargetPageType = targetPageType;
            Parameter = parameter;
        }
    }
}
