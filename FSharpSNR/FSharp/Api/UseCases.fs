namespace Api

module UseCases =

    module Registration =

        let start =
            Validation.account
            >> map Database.persistRegistration