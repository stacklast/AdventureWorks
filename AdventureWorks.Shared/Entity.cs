namespace AdventureWorks.Shared;
public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(Guid guid)
    {
        Rowguid = guid;
    }

    protected Entity(int id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public Guid Rowguid { get; init; }
    public int Id { get; init; }

    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}

