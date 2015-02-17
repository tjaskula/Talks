namespace Api

module UseCases =

    module Registration =

        let start =
            Validation.validateAll
            >> map Validation.normalizeEmail
            >> bind RegistrationService.shouldConfirmEmail
            >> bind Database.findByEmailRegistration
            >> bind RegistrationService.setActivationCode
            >> bind Database.persistRegistration
            >> bind Notification.sendActivationEmail