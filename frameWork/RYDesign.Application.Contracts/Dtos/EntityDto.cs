﻿namespace RYDesign.Application.Contracts.Dtos
{
    public class EntityDto<TKey>
    {
        public EntityDto()
        {

        }

        public EntityDto(TKey id)
        {
            Id = id;
        }

        public virtual TKey Id { get; set; }
    }
}
