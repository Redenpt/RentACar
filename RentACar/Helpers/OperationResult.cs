namespace RentACar.Helpers
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string? DataType { get; set; }
        public object? Data { get; set; }
        public List<string> Errors { get; set; }
        public bool OpenModal { get; set; } = false;
        public string ModalNameToOpen { get; set; } = string.Empty;
        public string OperationMessage { get; set; } = string.Empty;
        public string ViewBagKey { get; set; } = string.Empty;
        public OperationType OperationType { get; set; }

        public OperationResult() { }
        public OperationResult(bool success, object data, List<string> errors, OperationType operationType, bool openModal = false, string modalNameToOpen = "", string operationMessage = "", string viewBagKey = "")
        {
            Success = success;
            Data = data;
            DataType = data.GetType().AssemblyQualifiedName;
            Errors = errors;
            OpenModal = openModal;
            ModalNameToOpen = modalNameToOpen;
            OperationMessage = operationMessage;
            ViewBagKey = viewBagKey;
            OperationType = operationType;
        }

        //passa para "NONE" porque sei que foi uma ação finalizada com sucesso, ou não
        public OperationResult(bool success, string entityName, OperationType operationType, string operationFailedMessage = "")
        {
            Success = success;
            if (success)
            {
                switch (operationType)
                {
                    case OperationType.Create:
                        OperationMessage = $"{entityName} criado/a com sucesso!";
                        OperationType = OperationType.None;
                        break;

                    case OperationType.Update:
                        OperationMessage = $"{entityName} atualizado/a com sucesso!";
                        OperationType = OperationType.None;
                        break;

                    case OperationType.Delete:
                        OperationMessage = $"{entityName} eliminado/a com sucesso!";
                        OperationType = OperationType.None;
                        break;

                    case OperationType.Restore:
                        OperationMessage = $"{entityName} restaurado/a com sucesso!";
                        OperationType = OperationType.None;
                        break;
                }
            }
            else
            {
                OperationMessage = operationFailedMessage;
                OperationType = OperationType.None;
            }
        }
    }

    public enum OperationType
    {
        None = 0,

        Create = 1,
        Update = 2,
        Delete = 3,
        Restore = 4,

        Activate = 5,
        Deactivate = 6,

        SoftDelete = 7,
        HardDelete = 8
    }
}
