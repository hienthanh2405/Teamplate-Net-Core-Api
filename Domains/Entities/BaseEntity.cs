namespace Domains.Entities
{
    public abstract class BaseEntity<TId>
    {
        public virtual TId Id { get; set; }

        //public virtual byte[] Version { get; set; }

        //public override bool Equals(object obj)
        //{
        //    return Equals(obj as BaseEntity<TId>);
        //}

        //public virtual bool Equals(BaseEntity<TId> other)
        //{
        //    if (other == null)
        //    {
        //        return false;
        //    }

        //    if (ReferenceEquals(this, other))
        //    {
        //        return true;
        //    }

        //    if (IsTransient(this) || IsTransient(other) || !Equals(Id, other.Id))
        //    {
        //        return false;
        //    }
        //    var unproxiedType1 = other.GetUnproxiedType();
        //    var unproxiedType2 = GetUnproxiedType();
        //    return unproxiedType2.IsAssignableFrom(unproxiedType1) || unproxiedType1.IsAssignableFrom(unproxiedType2);
        //}

        //public override int GetHashCode()
        //{
        //    if (Equals(Id, default(TId)))
        //    {
        //        return base.GetHashCode();
        //    }
        //    return Id.GetHashCode();
        //}

        //private static bool IsTransient(BaseEntity<TId> obj)
        //{
        //    return obj != null && Equals(obj.Id, default(TId));
        //}

        //private Type GetUnproxiedType()
        //{
        //    return GetType();
        //}
    }
}