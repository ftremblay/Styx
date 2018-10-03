namespace RageCure.Utils

module QuickSort =    

    let private merge (l, p, r) = 
        List.concat [l; [p]; r]

    let private partition (lst : float32 list) =
        match lst with
        | pivotVal::xs -> 
            let folder ((l, r, i) as acc) next = 
                let addLeft ()                = List.append l [next]
                let addRight ()               = List.append r [next]
                let moveNextToLeftOfPivot ()  = (addLeft(), r, i + 1)
                let moveNextToRightOfPivot () = (l, addRight(), i)
                let nothingHappens ()         = acc
                let result = 
                    if      next < pivotVal then moveNextToLeftOfPivot()
                    else if next > pivotVal then moveNextToRightOfPivot()
                    else                         nothingHappens()
                result 

            let initAcc = ([], [], 0)
            xs 
            |> List.fold folder initAcc
            |> (fun (left, right, _) -> (left, pivotVal, right))

        | [] -> ([], 0.f, [])

    let rec sort lst : float32 list =
        if List.length lst > 1 then
            let (l, p, r) = partition lst
            let left =  sort l
            let right = sort r
            merge (left, p, right)
        else
            lst

