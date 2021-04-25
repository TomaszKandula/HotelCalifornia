export interface IGetRoomsInfo
{
    isLoading: boolean;
    roomsInfo: IRoomsInfo[];
}

export interface IRoomsInfo
{
    id: string;
    info: string;
}
