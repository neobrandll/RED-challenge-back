using System;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IDataProvider
    {
        public IEnumerable<Projections.OrderProjection> GetData();

        public Projections.OrderProjection FindData(Guid id);

        public Projections.OrderProjection CreateOrder(); // fill in inputs

        public void DeleteData(Guid id);
    }
}