import axios, { Method } from "axios";

interface IPromiseResult 
{
    status: number | null;
    data: any | null;
    error: any | null;
}

export const promiseDefaultResult: IPromiseResult = 
{
    status: null,
    data: null,
    error: null
}

export const ExecuteRequest = async (url: string, method: Method, payload?: any): Promise<IPromiseResult> =>
{
    let result: IPromiseResult = 
    { 
        status: null,
        data: null,
        error: null
    };

    await axios(url, 
    {
        method: method, 
        responseType: "json",
        data: payload
    })
    .then(response =>
    {
        result = 
        {
            status: response.status,
            data: response.data,
            error: null
        }
    })
    .catch(error =>
    {
        result = 
        { 
            status: null,
            data: null,
            error: error 
        };
    });

    return result;
}