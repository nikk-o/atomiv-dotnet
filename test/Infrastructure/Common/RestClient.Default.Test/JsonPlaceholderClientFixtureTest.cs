﻿using Xunit;

namespace Optivem.Platform.Infrastructure.Common.RestClient.Default.Test
{
    public class JsonPlaceholderClientFixtureTest : IClassFixture<JsonPlaceholderClientFixture>
    {
        public JsonPlaceholderClientFixtureTest(JsonPlaceholderClientFixture fixture)
        {
            JsonPlaceholderClient = fixture.JsonPlaceholderClient;
        }

        public JsonPlaceholderClient JsonPlaceholderClient { get; }
    }
}