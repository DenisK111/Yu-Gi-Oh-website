namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface ISoftDeleteService<TEntity>
    {
        public void SoftDelete(TEntity entity);
        public void Undelete(TEntity id);

    }
}
