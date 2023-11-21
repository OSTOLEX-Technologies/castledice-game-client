using casltedice_events_logic.ServerToClient;

namespace Src.NetworkingModule.DTOAccepters
{
    public interface IApproveMoveDTOAccepter
    {
        void AcceptApproveMoveDTO(ApproveMoveDTO dto);
    }
}