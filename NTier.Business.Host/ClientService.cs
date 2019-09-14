using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NTier.Business.Interface;
using NTier.Data.Interface.DataAccess;
using NTier.Data.Interface.UnitOfWork;
using NTier.Entities.Model;
using NTier.Entities.Validation;
using NTier.Entities.ViewModel.Operation;

namespace NTier.Business.Host
{
  public class ClientService : IClientManager
  {
    private readonly IUnitOfWork _unitOfWork;
    public ClientService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public OperationResponse<Client> GetClientByFilter(Expression<Func<Client, bool>> filter = null, Func<IQueryable<Client>, IOrderedQueryable<Client>> orderBy = null, string includedProperties = "")
    {
      OperationResponse<Client> response = new OperationResponse<Client>();
      try
      {
        response.Data = _unitOfWork.ClientRepository.Get(filter, orderBy, includedProperties);
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }

      return response;
    }

    public OperationResponse<Client> GetClientById(int id)
    {
      OperationResponse<Client> response = new OperationResponse<Client>();

      try
      {
        var existsClient = _unitOfWork.ClientRepository.GetById(id);
        if (existsClient == null)
        {
          throw new Exception("Cari bulunamadı");
        }

        response.Status = true;
        response.Data = existsClient;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }

      return response;
    }

    public OperationResponse<Client> SaveClient(Client clientEntity)
    {
      OperationResponse<Client> response = new OperationResponse<Client>();

      try
      {
        if (clientEntity == null)
        {
          throw new Exception("Client nesnesi boş olamaz.");
        }

        //Validation.
        var valid = new ClientValidation().Validate(clientEntity);
        if (valid.IsValid == false)
        {
          string errorMessages = "";
          foreach (var error in valid.Errors)
          {
            errorMessages += error.ErrorMessage + Environment.NewLine;
          }
          throw new Exception(errorMessages);
        }



        if (clientEntity.ClientId > 0)
        {
          //Update işlemi mi
          var existsClient = _unitOfWork.ClientRepository.GetById(clientEntity.ClientId);
          if (existsClient == null)
          {
            throw new Exception("Cari bulunamadı");
          }
        }

        if (clientEntity.ClientId > 0)
        {
          clientEntity.UpdatedDateTime = DateTime.Now;
          _unitOfWork.ClientRepository.Update(clientEntity);
        }
        else
        {
          clientEntity.CreatedDateTime = DateTime.Now;
          _unitOfWork.ClientRepository.Insert(clientEntity);
        }

        response.Data = clientEntity;
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }

      return response;
    }

    public OperationResponse<Client> DeleteClientById(int id)
    {
      OperationResponse<Client> response = new OperationResponse<Client>();

      try
      {
        var existsClient = _unitOfWork.ClientRepository.GetById(id);

        if (existsClient == null)
        {
          throw new Exception("Cari bulunamadı");
        }

        _unitOfWork.ClientRepository.Delete(existsClient);

        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }

      return response;
    }
  }
}
