namespace Api

module UseCases =

    module Registration =

        let start =
            Validation.validateAll
            >!> Validation.normalizeEmail
            >=> RegistrationService.tryConfirmEmail
            >=> RegistrationService.setActivationCode
            >=> Database.findByEmailRegistration
            >=> Database.persistRegistration
            >=> Notification.sendActivationEmail